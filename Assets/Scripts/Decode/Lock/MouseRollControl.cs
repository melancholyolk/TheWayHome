using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// 鼠标滚动控制
/// 挂在需要滚动的物体上
/// </summary>
[RequireComponent(typeof(Collider))]
public class MouseRollControl : MonoBehaviour
{
    public float speed;
    public float minAngel;
    public AudioClip roll;
    public float currentAngel;
    private bool _isMouse = true;
    private Vector3 _lastPos;

    private bool _thisRolling = false;
    private bool _isRolling = false;
    private float _angel;
    private int _unit = 0;
    private float ScrollSpeed = 0;

    private void Start()
    {
        minAngel = 360f / (transform.childCount - 1);
        _angel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _isMouse = true;
            Ray ray = GameObject.FindWithTag("DecodeCamera").GetComponent<Camera>()
                .ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    
                    if (_lastPos.Equals(Vector3.zero))
                    {
                        print("last" + _lastPos);
                        _lastPos = Input.mousePosition;
                        return;
                    } 
                    _thisRolling = true;
                    float y = Input.mousePosition.y - _lastPos.y;
                    transform.Rotate(Vector3.right * -y * speed, Space.Self);
                    _angel += y * speed;
                    _angel %= 360;
                    if (_unit != (int) (_angel / minAngel))
                    {
                        _unit = (int) (_angel / minAngel);
                        GetComponent<AudioSource>().PlayOneShot(roll);
                    }
                    _lastPos = Input.mousePosition;
                }
            }
        }


        if (Input.GetMouseButtonUp(0) && _isMouse && _thisRolling)
        {
            _lastPos = Vector3.zero;
            NormalizeAngel();
            SendAnswer();
            return;
        }

        
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (!_isRolling)
            {
                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    ScrollSpeed = -3+-minAngel / 8;
                }
                else
                {
                    ScrollSpeed = 3+minAngel / 8;
                }
            }

            Ray ray = GameObject.FindWithTag("DecodeCamera").GetComponent<Camera>()
                .ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    _isMouse = false;
                    ScrollSpeed += Input.GetAxis("Mouse ScrollWheel") * minAngel * 2 / 3;
                    _thisRolling = true;
                    _isRolling = true;
                }
            }
        }

        if (Mathf.Abs(ScrollSpeed) > 1f && !_isMouse && _thisRolling)
        {
            float y = ScrollSpeed;
            transform.Rotate(Vector3.right * -y * speed, Space.Self);
            _angel += y * speed;
            _angel %= 360;
            if (_unit != (int) (_angel / minAngel))
            {
                _unit = (int) (_angel / minAngel);
                GetComponent<AudioSource>().PlayOneShot(roll);
            }
        }

        if (Mathf.Abs(ScrollSpeed) <= 0.25f && !_isMouse)
        {
            NormalizeAngel();
           
            SendAnswer();
            _isRolling = false;
            _isMouse = true;
        }

        if (ScrollSpeed != 0f)
        {
            if (ScrollSpeed > 0) ScrollSpeed -= 0.5f;
            else
            {
                ScrollSpeed += 0.5f;
            }
        }
    }

    private void NormalizeAngel()
    {
        if (!_thisRolling) return;
        _thisRolling = false;
        _lastPos = Vector3.zero;
        _angel %= 360;
        int index = (int) ((_angel + minAngel/2) / minAngel);
        print(index);
        Quaternion r = transform.localRotation;
        r.x = 0;
        transform.localRotation = r;
        transform.Rotate(Vector3.right * (-index * minAngel), Space.Self);

        currentAngel = (-index * minAngel);
    }

    private void SendAnswer()
    {
        print("发送答案");
        var p = transform;
        for (int i = 0; i < 5; i++)
        {
            p = p.parent;
            if (p.GetComponent<RollInput>())
            {
                p.SendMessage("SendAnswer");
                break;
            }
        }
    }

    public float GetAngel()
    {
        return _angel;
    }
}