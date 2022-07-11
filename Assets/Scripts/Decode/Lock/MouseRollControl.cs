using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Decode;
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
    public int index;
    public int total;
    public RollInput input;
    private bool _isMouse = true;
    private Vector3 _lastPos;

    private bool _thisRolling = false;
    private bool _isRolling = false;
    private float _angel;
    private int _unit = 0;
    private float _scrollSpeed = 0;

    private void Start()
    {
	    total = transform.childCount - 1;
        minAngel = 360f / total;
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
                    _scrollSpeed = -3+-minAngel / 8;
                }
                else
                {
                    _scrollSpeed = 3+minAngel / 8;
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
                    _scrollSpeed += Input.GetAxis("Mouse ScrollWheel") * minAngel * 2 / 3;
                    _thisRolling = true;
                    _isRolling = true;
                }
            }
        }

        if (Mathf.Abs(_scrollSpeed) > 1f && !_isMouse && _thisRolling)
        {
            float y = _scrollSpeed;
            transform.Rotate(Vector3.right * -y * speed, Space.Self);
            _angel += y * speed;
            _angel %= 360;
            if (_unit != (int) (_angel / minAngel))
            {
                _unit = (int) (_angel / minAngel);
                GetComponent<AudioSource>().PlayOneShot(roll);
            }
        }

        if (Mathf.Abs(_scrollSpeed) <= 0.25f && !_isMouse)
        {
            NormalizeAngel();
           
            SendAnswer();
            _isRolling = false;
            _isMouse = true;
        }

        if (_scrollSpeed != 0f)
        {
            if (_scrollSpeed > 0) _scrollSpeed -= 0.5f;
            else
            {
                _scrollSpeed += 0.5f;
            }
        }
    }

    private void NormalizeAngel()
    {
        if (!_thisRolling) return;
        _thisRolling = false;
        _lastPos = Vector3.zero;
        _angel %= 360;
        index = (int) ((_angel + minAngel/2) / minAngel);
        Quaternion r = transform.localRotation;
        r.x = 0;
        transform.localRotation = r;
        transform.Rotate(Vector3.right * (-index * minAngel), Space.Self);

        currentAngel = (-index * minAngel);
    }

    private void SendAnswer()
    {
        print("发送答案");
        input.CheckInput();
    }

  
}