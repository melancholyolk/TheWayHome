using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制摄像机转场
/// </summary>
public class StartScene_Camera : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;
    public float duration;
    
    private Vector3 s_tar;
    private Vector3 v = Vector3.zero;
    [SerializeField] private bool StartChange = false;
    private void OnDrawGizmos()
    {
        s_tar = offset + player.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        s_tar = offset + player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartChange)
        {
            if (Vector3.Distance(s_tar,transform.position) <= 0)
            {
                v = Vector3.zero;
                StartChange = false;
                return;
            }
            //同步旋转
            transform.rotation = player.rotation;
            Vector3 currentpos = transform.position;
            Vector3 nextpos = Vector3.one;

            nextpos.x = Mathf.SmoothDamp(currentpos.x, s_tar.x, ref v.x, duration);
            nextpos.y = Mathf.SmoothDamp(currentpos.y, s_tar.y, ref v.y, duration);
            nextpos.z = Mathf.SmoothDamp(currentpos.z, s_tar.z, ref v.z, duration);

            transform.position = nextpos;
        }
    }

    public void StartConvert()
    {
        StartChange = true;
    }
}
