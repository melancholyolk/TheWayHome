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
public class PlayerMove : NetworkBehaviour
{
    [FormerlySerializedAs("obj_pos")]
    public Transform objPosition;
    
    public List<GameObject> handles;

    [FormerlySerializedAs("is_local")]
     public bool isLocal;
    [ShowInInspector,SerializeField]
    private List<int> condition;
    
    public GameObject[] players;

    public Material playerMaterial;

    public Hand hand;

    public PlayerAttribute m_attribute;

    public bool useLocalPosition = false;
    
    public CharacterProperty property;
    
    public State curState = State.None;
    public enum State
    {
        None,
        LossHp,
        LossWarm,
        NeedHelp
    }
    
    private Rigidbody m_Rigidbody;

    private float m_TempSpeed;

    private PlayerAnimatorControl m_AnimatorControl;

    private bool m_IsWalking = false;

    private Vector3 m_OriginScale;

    private int m_CurrentPlayer = 0;

    private int m_CurrentDirection = -1; //面向北方为0顺时针递加

    private int m_CurrentWindDirection = -1;

    private float m_CurrentWindForce = 0;

    private bool m_ChangeMotion;

    private GameObject[] m_HoldObject;

    private bool m_IsHold = false;

    private NetworkAnimator m_NetworkAnimator;

    
    private enum PlayerState
    {
        NORMAL = 0,
        HARD = 1,
        TRAILWIND = 2,
    }

    private PlayerState m_CurrentState = PlayerState.NORMAL;

    public enum Dir
    {
        North,
        Sorth,
        East,
        West,

	}


    public void Awake()
    {
        m_OriginScale = transform.localScale;
        m_TempSpeed = property.curMoveSpeed;
        condition = new List<int>();
        m_HoldObject = new GameObject[2];
        m_Rigidbody = GetComponent<Rigidbody>();
        m_NetworkAnimator = GetComponent<NetworkAnimator>();
        m_AnimatorControl = GetComponent<PlayerAnimatorControl>();
        CmdInitComponent(m_CurrentPlayer, Vector3.one, m_IsHold);
        PlayerManager.Instance.SetCondition(condition);
    }

	// Update is called once per frame
	void Update()
    {
	    if (!isLocal)
        {
            return;
        }
     
        if (!OperationControl.Instance.CanOperate())
        {
            return;
        }
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");
        Vector3 move_speed = new Vector3(moveX, 0, moveY).normalized;
        if(useLocalPosition)
            move_speed = Quaternion.AngleAxis(-45,Vector3.up) * move_speed ;
        m_Rigidbody.velocity = move_speed * m_TempSpeed;
        PlayAnimation(move_speed);
    }

    void InitComponent(int cur, Vector3 scale, bool hold)
    {
        transform.localScale = scale;
        players[1 - cur].transform.localPosition = Vector3.down * 10000;
        players[cur].transform.localPosition = Vector3.zero;
        m_AnimatorControl.SetAnimator(cur);
        m_NetworkAnimator.animator = m_AnimatorControl.GetAnimator();
        CheckHold(hold);
    }

    #region Cmd
    [Command]
    private void CmdSetAnimation(PlayerAnimatorControl.AnimationName ani)
    {
        RpcSetAnimation(ani);
    }

    [ClientRpc]
    private void RpcSetAnimation(PlayerAnimatorControl.AnimationName ani)
    {
        if(!isLocal)
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
        if (!isLocal)
            m_AnimatorControl.SetAnimation(ani, time);
    }

    [Command(requiresAuthority =  false)]
    private void CmdInitComponent(int currentPlayer, Vector3 scale, bool hold)
    {
        RpcInitComponent(currentPlayer, scale, hold);
    }

    [ClientRpc]
    private void RpcInitComponent(int currentPlayer, Vector3 scale, bool hold)
    {
    if(!isLocal)
        InitComponent(currentPlayer, scale, hold);
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

    [Command]
    private void CmdDiscardProp(int num)
    {
        RpcDiscardProp(num);
    }
    [ClientRpc]
    private void RpcDiscardProp(int num)
    {
        GameObject obj = ObjectPool._instance.GetGO(num);
        // obj.GetComponent<PropPick>().SetPropInfo(num);
        obj.transform.position = objPosition.position;
        obj.transform.localEulerAngles = new Vector3(45, -45, 0);
    }

    [Command]
    public void CmdDestory(string obj)
    {
        RpcDestory(obj);
    }

    [ClientRpc]
    private void RpcDestory(string obj)
    {
	    Debug.LogError( GameObject.Find(obj));
        GameObject.Find(obj).GetComponent<PropPick>().is_pick = true;
    }
    #endregion

    #region Animator&Move
    void PlayAnimation(Vector3 speed)
    {
        var last = m_CurrentPlayer;
        var lastDirect = m_CurrentDirection;
        var scale = transform.localScale;
        if (speed == Vector3.zero)
        {
            if (m_IsWalking)
            {
                m_AnimatorControl.SetAnimation(PlayerAnimatorControl.AnimationName.IDLE);
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
                m_AnimatorControl.SetAnimation(ChoosePlayerState(), 0.06f);
                CmdSetAnimation1(ChoosePlayerState(), 0.06f);
                m_IsWalking = true;
            }

            if (last != m_CurrentPlayer || lastDirect != m_CurrentDirection)
            {
                InitComponent(m_CurrentPlayer, scale, m_IsHold);
                m_AnimatorControl.SetAnimation(ChoosePlayerState(), 0);
                CmdInitComponent(m_CurrentPlayer, scale, m_IsHold);
                CmdSetAnimation1(ChoosePlayerState(), 0);
            }
            else if (m_ChangeMotion)
            {
                m_AnimatorControl.SetAnimation(ChoosePlayerState());
                CmdSetAnimation(ChoosePlayerState());
            }
            else
            {
                var ani = ChoosePlayerState();
                if (ani != m_AnimatorControl.currentAnimation)
                {
                    m_AnimatorControl.SetAnimation(ani);
                    CmdSetAnimation(ani);
                }
            }
        }

        m_ChangeMotion = false;
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
        string direction = (string)hashtable["direct"];
        direction = direction.ToLower();
        m_CurrentWindForce = (float)hashtable["force"];
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
            m_TempSpeed = property.curMoveSpeed;
            return PlayerAnimatorControl.AnimationName.WALK;
        }

        if (m_CurrentDirection == m_CurrentWindDirection || m_CurrentDirection == m_CurrentWindDirection + 1 ||
            m_CurrentDirection == m_CurrentWindDirection + 7)
        {
            m_TempSpeed = property.curMoveSpeed + m_CurrentWindForce * property.curMoveSpeed;
            return PlayerAnimatorControl.AnimationName.TRAILWIND;
        }

        else
        {
            m_TempSpeed = property.curMoveSpeed - m_CurrentWindForce * property.curMoveSpeed;
            return PlayerAnimatorControl.AnimationName.WALKHARD;
        }
    }
    #endregion

    #region PlayerCondition
    public void AddCondition(int str)
    {
        condition.Add(str);
    }

    public void AddCondition(List<int> c)
    {
	    condition.AddRange(c);
    }
    public void SetCondition(List<int> c)
    {
	    condition.Clear();
	    c.CopyTo(condition);
    }
    public void RemoveCondition(int str)
    {
        condition.Remove(str);
    }

    public bool JudgeCondition(int cdt)
    {
        if (condition.Contains(cdt))
        {
            return true;
        }

        return false;
    }
    #endregion
    private void HoldObject(int index)
    {
        if (m_IsHold)
        {
            print("已经持有道具");
            return;
        }

        m_IsHold = true;
        m_AnimatorControl.PartialAnimationInit(PlayerAnimatorControl.AnimationName.HOLD);
        m_HoldObject[0] = Instantiate(handles[index], hand.rightHandFront.position, Quaternion.Euler(new Vector3(45, -45, -32)));
        m_HoldObject[0].transform.parent = hand.rightHandFront;
        m_HoldObject[0].GetComponent<SortingGroup>().sortingOrder = -6;
        m_HoldObject[1] = Instantiate(handles[index], hand.rightHandBack.position, Quaternion.Euler(new Vector3(45, -45, -32)));
        m_HoldObject[1].transform.parent = hand.rightHandBack;
        m_HoldObject[1].GetComponent<SortingGroup>().sortingOrder = 11;

    }

    private void PutDownObject()
    {
        if (!m_IsHold)
        {
            Debug.LogError("还没有持有道具");
            return;
        }

        m_IsHold = false;
        m_AnimatorControl.PartialAnimationInit(PlayerAnimatorControl.AnimationName.PUTDOWN);
        Destroy(m_HoldObject[0]);
        Destroy(m_HoldObject[1]);
    }

    public void DiscarderProp(PropInfo info)
    {
        CmdDiscardProp(info.prop_id);
        RemoveCondition(info.prop_id);
    }

}

[Serializable]
public class Hand
{
    public Transform leftHandFront;

    public Transform rightHandFront;

    public Transform leftHandBack;

    public Transform rightHandBack;
}
[Serializable]
public class PlayerAttribute 
{
    [FormerlySerializedAs("moveSpeed")] 
    public float maxMoveSpeed;

    public int maxHp;

    public int maxWarm;     //100

}
[Serializable]
public struct CharacterProperty
{
    public float curMoveSpeed;
    public int curHP;
    public int curWarm;
    public int curOutsideTemperature;

    public float warmChangeSpeed;
    public float healthChangeSpeed;
}
