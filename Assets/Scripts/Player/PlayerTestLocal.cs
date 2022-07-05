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
public class PlayerTestLocal : MonoBehaviour
{
    public Transform obj_pos;
    
    public List<GameObject> handles;

    public bool is_local;
    
    public List<string> condition;

    public float speed = 5f;

    [FormerlySerializedAs("players")] 
    public GameObject[] playerSprites;

    public GameObject propPrefab;

    public Material playerMaterial;

    public Transform leftHandFront;

    public Transform rightHandFront;

    public Transform leftHandBack;

    public Transform rightHandBack;

    private Rigidbody _rigidbody;

    private PlayerAnimatorControl _animatorControl;

    private bool _isWalking = false;

    private Vector3 _originScale;

    private int _currentPlayer = 0;

    private int _currentDirection = -1; //面向北方为0顺时针递加

    private int _currentWindDirection = -1;

    private float _currentWindForce = 0;

    private float _currentSpeed;

    private bool _changeMotion;

    private GameObject[] _holdObject;

    private bool _isHold = false;

    private Animator _networkAnimator;

    private CanvasManager _canvasManager;

    private enum PlayerState
    {
        NORMAL = 0,
        HARD = 1,
        TRAILWIND = 2,
    }

    private PlayerState _currentState = PlayerState.NORMAL;

    private void Awake()
    {
	    Start();
    }


    // Start is called before the first frame update
    void Start()
    {
        _originScale = transform.localScale;
        _currentSpeed = speed;
        condition = new List<string>();
        _holdObject = new GameObject[2];
        _rigidbody = GetComponent<Rigidbody>();
        _networkAnimator = GetComponent<Animator>();
        _animatorControl = GetComponent<PlayerAnimatorControl>();
        CmdInitComponent(_currentPlayer, Vector3.one, _isHold);
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_local)
        {
            return;
        }

        if (!_canvasManager.CanOperate())
        {
            return;
        }
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");
        Vector3 move_speed = new Vector3(moveX, 0, moveY).normalized;
        _rigidbody.velocity = move_speed * _currentSpeed;
        PlayAnimation(move_speed);
    }


    public void CmdDecodeIsUse(SyncDecodeInfo obj, bool temp)
    {
        obj.isUsing = temp;
    }

    public void CmdDecodeIsComplete(SyncDecodeInfo obj, bool temp)
    {
        obj.isComplete = temp;
    }
    
    public void CmdPropIsPick(GameObject obj)
    {
        obj.GetComponent<PropProperty>().is_pick = true;
    }

    void PlayAnimation(Vector3 speed)
    {
        var last = _currentPlayer;
        var lastDirect = _currentDirection;
        var scale = transform.localScale;
        if (speed == Vector3.zero)
        {
            if (_isWalking)
            {
                CmdSetAnimation(PlayerAnimatorControl.AnimationName.IDLE);
                _isWalking = false;
            }
        }
        else
        {
            //z正方向为北，x正方向为东
            // 北
            if (speed.x == 0 && speed.z > 0)
            {
                scale = _originScale;
                _currentPlayer = 1;
                _currentDirection = 0;
            }
            //东北
            else if (speed.x > 0 && speed.z > 0)
            {
                scale = _originScale;
                scale.x *= -1;

                _currentPlayer = 0;
                _currentDirection = 1;
            }
            // 东
            else if (speed.x > 0 && speed.z == 0)
            {
                scale = _originScale;
                scale.x *= -1;

                _currentPlayer = 0;
                _currentDirection = 2;
            }
            //东南
            else if (speed.x > 0 && speed.z < 0)
            {
                scale = _originScale;
                scale.x *= -1;
                _currentPlayer = 0;
                _currentDirection = 3;
            }
            // 南
            else if (speed.x == 0 && speed.z < 0)
            {
                scale = _originScale;
                _currentPlayer = 0;
                _currentDirection = 4;
            }
            // 西南
            else if (speed.x < 0 && speed.z < 0)
            {
                scale = _originScale;
                _currentPlayer = 0;
                _currentDirection = 5;
            }
            // 西
            else if (speed.x < 0 && speed.z == 0)
            {
                scale = _originScale;
                scale.x *= -1;

                _currentPlayer = 1;
                _currentDirection = 6;
            }
            // 西北
            else if (speed.x < 0 && speed.z > 0)
            {
                scale = _originScale;
                scale.x *= -1;
                _currentPlayer = 1;
                _currentDirection = 7;
            }

            if (!_isWalking)
            {
                CmdSetAnimation1(ChoosePlayerState(), 0.06f);
                _isWalking = true;
            }

            if (last != _currentPlayer || lastDirect != _currentDirection)
            {
                CmdInitComponent(_currentPlayer, scale, _isHold);
                CmdSetAnimation1(ChoosePlayerState(), 0);
            }
            else if (_changeMotion)
            {
                CmdSetAnimation(ChoosePlayerState());
            }
            else
            {
                var ani = ChoosePlayerState();
                if (ani != _animatorControl.currentAnimation)
                {
                    CmdSetAnimation(ani);
                }
            }
        }

        _changeMotion = false;
    }
    
    private void CmdSetAnimation(PlayerAnimatorControl.AnimationName ani)
    {
        RpcSetAnimation(ani);
    }
    
    private void RpcSetAnimation(PlayerAnimatorControl.AnimationName ani)
    {
        _animatorControl.SetAnimation(ani);
    }

    
    private void CmdSetAnimation1(PlayerAnimatorControl.AnimationName ani, float time)
    {
        RpcSetAnimation1(ani, time);
    }
    
    private void RpcSetAnimation1(PlayerAnimatorControl.AnimationName ani, float time)
    {
        _animatorControl.SetAnimation(ani, time);
    }
    
    private void CmdInitComponent(int currentPlayer, Vector3 scale, bool hold)
    {
        RpcInitComponent(currentPlayer, scale, hold);
    }
    
    private void RpcInitComponent(int currentPlayer, Vector3 scale, bool hold)
    {
        InitComponent(currentPlayer, scale, hold);
    }

    void InitComponent(int cur, Vector3 scale, bool hold)
    {
        transform.localScale = scale;
        playerSprites[1 - cur].transform.localPosition = Vector3.down * 100;
        playerSprites[cur].transform.localPosition = Vector3.zero;
        _animatorControl.SetAnimator(cur);
        _networkAnimator = _animatorControl.GetAnimator();
        CheckHold(hold);
    }

    private void CheckHold(bool hold)
    {
        if (hold)
        {
            _animatorControl.PartialAnimation(PlayerAnimatorControl.AnimationName.HOLD);
        }
        else
        {
            _animatorControl.PartialAnimation(PlayerAnimatorControl.AnimationName.PUTDOWN);
        }
    }

    public void ChangeState(Hashtable hashtable)
    {
        string direction = (string) hashtable["direct"];
        direction = direction.ToLower();
        _currentWindForce = (float) hashtable["force"];
        var lastDirect = _currentWindDirection;
        switch (direction)
        {
            case "north":
                _currentWindDirection = 0;
                break;
            case "northeast":
                _currentWindDirection = 1;
                break;
            case "east":
                _currentWindDirection = 2;
                break;
            case "southeast":
                _currentWindDirection = 3;
                break;
            case "south":
                _currentWindDirection = 4;
                break;
            case "southwest":
                _currentWindDirection = 5;
                break;
            case "west":
                _currentWindDirection = 6;
                break;
            case "northwest":
                _currentWindDirection = 7;
                break;
            default:
                _currentWindDirection = -1;
                break;
        }

        if (lastDirect != _currentWindDirection) _changeMotion = true;
    }

    private PlayerAnimatorControl.AnimationName ChoosePlayerState()
    {
        if (_currentWindDirection == -1)
        {
            _currentSpeed = speed;
            return PlayerAnimatorControl.AnimationName.WALK;
        }

        if (_currentDirection == _currentWindDirection || _currentDirection == _currentWindDirection + 1 ||
            _currentDirection == _currentWindDirection + 7)
        {
            _currentSpeed = speed + _currentWindForce * speed;
            return PlayerAnimatorControl.AnimationName.TRAILWIND;
        }

        else
        {
            _currentSpeed = speed - _currentWindForce * speed;
            return PlayerAnimatorControl.AnimationName.WALKHARD;
        }
    }


    public void CmdHoldObject(int index)
    {
       RpcHoldObject(index);
    }


    private void RpcHoldObject(int index)
    {
        HoldObject(index);
    }

    private void HoldObject(int index)
    {
        if (_isHold)
        {
            print("已经持有道具");
            return;
        }

        _isHold = true;
        _animatorControl.PartialAnimationInit(PlayerAnimatorControl.AnimationName.HOLD);
        _holdObject[0] = Instantiate(handles[index], rightHandFront.position, Quaternion.Euler(new Vector3(45, -45, -32)));
        _holdObject[0].transform.parent = rightHandFront;
        _holdObject[0].GetComponent<SortingGroup>().sortingOrder = -6;
        _holdObject[1] = Instantiate(handles[index], rightHandBack.position, Quaternion.Euler(new Vector3(45, -45, -32)));
        _holdObject[1].transform.parent = rightHandBack;
        _holdObject[1].GetComponent<SortingGroup>().sortingOrder = 11;
        
    }

    private void PutDownObject()
    {
        if (!_isHold)
        {
            print("还没有持有道具");
            return;
        }

        _isHold = false;
        _animatorControl.PartialAnimationInit(PlayerAnimatorControl.AnimationName.PUTDOWN);
        Destroy(_holdObject[0]);
        Destroy(_holdObject[1]);
    }


    public void DiscarderProp(PropInfo info)
    {
        CmdDiscardProp(int.Parse(info.prop_number));
        RemoveCondition(info.prop_name);
    }

   
    private void CmdDiscardProp(int num)
    {
        RpcDiscardProp(num);
    }

    private void RpcDiscardProp(int num)
    {
        GameObject obj = Instantiate(propPrefab);
        obj.GetComponent<PropPick>().SetPropInfo(num);
        obj.transform.position = obj_pos.position;
        obj.transform.localEulerAngles = new Vector3(45,-45,0);
        obj.GetComponent<SpriteRenderer>().sortingOrder = playerSprites[0].GetComponent<SortingGroup>().sortingOrder;
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
    
    public void CmdDestory(string obj)
    {
        RpcDestory(obj);
    }
    
    private void RpcDestory(string obj)
    {
        GameObject.Find(obj).GetComponent<PropPick>().is_pick = true;
    }
}