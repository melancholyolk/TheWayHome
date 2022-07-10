using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

/// <summary>
/// 风力-1,1
/// -1不能前进
/// 0原速度
/// 1二倍原速度
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : NetworkBehaviour
{
    [FormerlySerializedAs("obj_pos")]
    public Transform objPosition;
    
    public List<GameObject> handles;

    [FormerlySerializedAs("is_local")] public bool isLocal;
    
    public List<string> condition;

    public float speed = 5f;

    public GameObject[] players;

    public GameObject propPrefab;

    public Material playerMaterial;

    public Transform leftHandFront;

    public Transform rightHandFront;

    public Transform leftHandBack;

    public Transform rightHandBack;

    private Rigidbody m_Rigidbody;

    private PlayerAnimatorControl m_AnimatorControl;

    private bool m_IsWalking = false;

    private Vector3 m_OriginScale;

    private int m_CurrentPlayer = 0;

    private int m_CurrentDirection = -1; //面向北方为0顺时针递加

    private int m_CurrentWindDirection = -1;

    private float m_CurrentWindForce = 0;

    private float m_CurrentSpeed;

    private bool m_ChangeMotion;

    private GameObject[] m_HoldObject;

    private bool m_IsHold = false;

    private NetworkAnimator m_NetworkAnimator;

    private CanvasManager m_CanvasManager;

    private enum PlayerState
    {
        NORMAL = 0,
        HARD = 1,
        TRAILWIND = 2,
    }

    private PlayerState m_CurrentState = PlayerState.NORMAL;

    private void Awake()
    {
        m_CanvasManager = GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        m_OriginScale = transform.localScale;
        m_CurrentSpeed = speed;
        condition = new List<string>();
        m_HoldObject = new GameObject[2];
        m_Rigidbody = GetComponent<Rigidbody>();
        m_NetworkAnimator = GetComponent<NetworkAnimator>();
        m_AnimatorControl = GetComponent<PlayerAnimatorControl>();
        CmdInitComponent(m_CurrentPlayer, Vector3.one, m_IsHold);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocal)
        {
            return;
        }

        if (!m_CanvasManager.CanOperate())
        {
            return;
        }
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");
        Vector3 move_speed = new Vector3(moveX, 0, moveY).normalized;
        m_Rigidbody.velocity = move_speed * m_CurrentSpeed;
        PlayAnimation(move_speed);
    }

    [Command]
    public void CmdDecodeIsUse(SyncItem obj, bool temp)
    {
        obj.isUsing = temp;
    }

    [Command]
    public void CmdDecodeIsComplete(SyncItem obj, bool temp)
    {
        obj.isComplete = temp;
    }

    [Command]
    public void CmdPropIsPick(GameObject obj)
    {
        obj.GetComponent<PropProperty>().is_pick = true;
    }

    void PlayAnimation(Vector3 speed)
    {
        var last = m_CurrentPlayer;
        var lastDirect = m_CurrentDirection;
        var scale = transform.localScale;
        if (speed == Vector3.zero)
        {
            if (m_IsWalking)
            {
                CmdSetAnimation(PlayerAnimatorControl.AnimationName.IDLE);
                m_IsWalking = false;
            }
        }
        else
        {
            //z正方向为北，x正方向为东
            // 北
            if (speed.x == 0 && speed.z > 0)
            {
                scale = m_OriginScale;
                m_CurrentPlayer = 1;
                m_CurrentDirection = 0;
            }
            //东北
            else if (speed.x > 0 && speed.z > 0)
            {
                scale = m_OriginScale;
                scale.x *= -1;

                m_CurrentPlayer = 0;
                m_CurrentDirection = 1;
            }
            // 东
            else if (speed.x > 0 && speed.z == 0)
            {
                scale = m_OriginScale;
                scale.x *= -1;

                m_CurrentPlayer = 0;
                m_CurrentDirection = 2;
            }
            //东南
            else if (speed.x > 0 && speed.z < 0)
            {
                scale = m_OriginScale;
                scale.x *= -1;
                m_CurrentPlayer = 0;
                m_CurrentDirection = 3;
            }
            // 南
            else if (speed.x == 0 && speed.z < 0)
            {
                scale = m_OriginScale;
                m_CurrentPlayer = 0;
                m_CurrentDirection = 4;
            }
            // 西南
            else if (speed.x < 0 && speed.z < 0)
            {
                scale = m_OriginScale;
                m_CurrentPlayer = 0;
                m_CurrentDirection = 5;
            }
            // 西
            else if (speed.x < 0 && speed.z == 0)
            {
                scale = m_OriginScale;
                scale.x *= -1;

                m_CurrentPlayer = 1;
                m_CurrentDirection = 6;
            }
            // 西北
            else if (speed.x < 0 && speed.z > 0)
            {
                scale = m_OriginScale;
                scale.x *= -1;
                m_CurrentPlayer = 1;
                m_CurrentDirection = 7;
            }

            if (!m_IsWalking)
            {
                CmdSetAnimation1(ChoosePlayerState(), 0.06f);
                m_IsWalking = true;
            }

            if (last != m_CurrentPlayer || lastDirect != m_CurrentDirection)
            {
                CmdInitComponent(m_CurrentPlayer, scale, m_IsHold);
                CmdSetAnimation1(ChoosePlayerState(), 0);
            }
            else if (m_ChangeMotion)
            {
                CmdSetAnimation(ChoosePlayerState());
            }
            else
            {
                var ani = ChoosePlayerState();
                if (ani != m_AnimatorControl.currentAnimation)
                {
                    CmdSetAnimation(ani);
                }
            }
        }

        m_ChangeMotion = false;
    }

    [Command]
    private void CmdSetAnimation(PlayerAnimatorControl.AnimationName ani)
    {
        RpcSetAnimation(ani);
    }

    [ClientRpc]
    private void RpcSetAnimation(PlayerAnimatorControl.AnimationName ani)
    {
        m_AnimatorControl.SetAnimation(ani);
    }


    [Command]
    private void CmdSetAnimation1(PlayerAnimatorControl.AnimationName ani, float time)
    {
        RpcSetAnimation1(ani, time);
    }

    [ClientRpc]
    private void RpcSetAnimation1(PlayerAnimatorControl.AnimationName ani, float time)
    {
        m_AnimatorControl.SetAnimation(ani, time);
    }

    [Command]
    private void CmdInitComponent(int currentPlayer, Vector3 scale, bool hold)
    {
        RpcInitComponent(currentPlayer, scale, hold);
    }

    [ClientRpc]
    private void RpcInitComponent(int currentPlayer, Vector3 scale, bool hold)
    {
        InitComponent(currentPlayer, scale, hold);
    }

    void InitComponent(int cur, Vector3 scale, bool hold)
    {
        transform.localScale = scale;
        players[1 - cur].transform.localPosition = Vector3.down * 100;
        players[cur].transform.localPosition = Vector3.zero;
        m_AnimatorControl.SetAnimator(cur);
        m_NetworkAnimator.animator = m_AnimatorControl.GetAnimator();
        CheckHold(hold);
    }

    private void CheckHold(bool hold)
    {
        if (hold)
        {
            m_AnimatorControl.PartialAnimation(PlayerAnimatorControl.AnimationName.HOLD);
        }
        else
        {
            m_AnimatorControl.PartialAnimation(PlayerAnimatorControl.AnimationName.PUTDOWN);
        }
    }

    public void ChangeState(Hashtable hashtable)
    {
        string direction = (string) hashtable["direct"];
        direction = direction.ToLower();
        m_CurrentWindForce = (float) hashtable["force"];
        var lastDirect = m_CurrentWindDirection;
        switch (direction)
        {
            case "north":
                m_CurrentWindDirection = 0;
                break;
            case "northeast":
                m_CurrentWindDirection = 1;
                break;
            case "east":
                m_CurrentWindDirection = 2;
                break;
            case "southeast":
                m_CurrentWindDirection = 3;
                break;
            case "south":
                m_CurrentWindDirection = 4;
                break;
            case "southwest":
                m_CurrentWindDirection = 5;
                break;
            case "west":
                m_CurrentWindDirection = 6;
                break;
            case "northwest":
                m_CurrentWindDirection = 7;
                break;
            default:
                m_CurrentWindDirection = -1;
                break;
        }

        if (lastDirect != m_CurrentWindDirection) m_ChangeMotion = true;
    }

    private PlayerAnimatorControl.AnimationName ChoosePlayerState()
    {
        if (m_CurrentWindDirection == -1)
        {
            m_CurrentSpeed = speed;
            return PlayerAnimatorControl.AnimationName.WALK;
        }

        if (m_CurrentDirection == m_CurrentWindDirection || m_CurrentDirection == m_CurrentWindDirection + 1 ||
            m_CurrentDirection == m_CurrentWindDirection + 7)
        {
            m_CurrentSpeed = speed + m_CurrentWindForce * speed;
            return PlayerAnimatorControl.AnimationName.TRAILWIND;
        }

        else
        {
            m_CurrentSpeed = speed - m_CurrentWindForce * speed;
            return PlayerAnimatorControl.AnimationName.WALKHARD;
        }
    }

    [Command]
    public void CmdHoldObject(int index)
    {
       RpcHoldObject(index);
    }

    [ClientRpc]
    private void RpcHoldObject(int index)
    {
        HoldObject(index);
    }

    private void HoldObject(int index)
    {
        if (m_IsHold)
        {
            print("已经持有道具");
            return;
        }

        m_IsHold = true;
        m_AnimatorControl.PartialAnimationInit(PlayerAnimatorControl.AnimationName.HOLD);
        m_HoldObject[0] = Instantiate(handles[index], rightHandFront.position, Quaternion.Euler(new Vector3(45, -45, -32)));
        m_HoldObject[0].transform.parent = rightHandFront;
        m_HoldObject[0].GetComponent<SortingGroup>().sortingOrder = -6;
        m_HoldObject[1] = Instantiate(handles[index], rightHandBack.position, Quaternion.Euler(new Vector3(45, -45, -32)));
        m_HoldObject[1].transform.parent = rightHandBack;
        m_HoldObject[1].GetComponent<SortingGroup>().sortingOrder = 11;
        
    }

    private void PutDownObject()
    {
        if (!m_IsHold)
        {
            print("还没有持有道具");
            return;
        }

        m_IsHold = false;
        m_AnimatorControl.PartialAnimationInit(PlayerAnimatorControl.AnimationName.PUTDOWN);
        Destroy(m_HoldObject[0]);
        Destroy(m_HoldObject[1]);
    }


    public void DiscarderProp(PropInfo info)
    {
        CmdDiscardProp(int.Parse(info.prop_number));
        RemoveCondition(info.prop_name);
    }

    [Command]
    private void CmdDiscardProp(int num)
    {
        RpcDiscardProp(num);
    }
    [ClientRpc]
    private void RpcDiscardProp(int num)
    {
        GameObject obj = ObjectPool._instance.GetGO();
        obj.GetComponent<PropPick>().SetPropInfo(num);
        obj.transform.position = objPosition.position;
        obj.transform.localEulerAngles = new Vector3(45,-45,0);
        obj.GetComponent<SpriteRenderer>().sortingOrder = players[0].GetComponent<SortingGroup>().sortingOrder;
    }

    public void AddCondition(string str)
    {
        condition.Add(str);
    }

    public void RemoveCondition(string str)
    {
        condition.Remove(str);
    }

    public bool JudgeCondition(string cdt)
    {
        if (condition.Contains(cdt))
        {
            return true;
        }

        return false;
    }

    [Command]
    public void CmdDestory(string obj)
    {
        RpcDestory(obj);
    }
    
    [ClientRpc]
    private void RpcDestory(string obj)
    {
        GameObject.Find(obj).GetComponent<PropPick>().is_pick = true;
    }
}