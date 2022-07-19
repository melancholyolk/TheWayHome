using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在密码锁周围放置物体
/// 放在密码锁上
/// </summary>
public class SetPosition : MonoBehaviour
{
    public GameObject[] objetcs;

    public float radius;
    private void OnDrawGizmos()
    {
        if(objetcs == null) return;
        int num = objetcs.Length;
        float angel = 360f / num;
        for (int i = 0; i < num; i++)
        {
            // objetcs[i].transform.rotation = new Quaternion(0,0,0,0);
            Vector3 forward = transform.forward.normalized;
            float localangel = angel * i;
            // if(i*localangel > 180) localangel =  angel * i - angel / 2;
            Vector3 rotation = new Vector3(-localangel,180,0);
            objetcs[i].transform.position = Quaternion.Euler(rotation) * forward * radius + transform.position;
            objetcs[i].transform.rotation = Quaternion.Euler(rotation );
            
            // if(i*angel < 180)
            //     objetcs[i].transform.rotation = Quaternion.Euler(rotation) * Quaternion.AngleAxis(90,transform.right);
            // else
            {
                objetcs[i].transform.rotation = Quaternion.Euler(rotation) * Quaternion.AngleAxis(90,transform.right) * Quaternion.AngleAxis(180,transform.forward);
            }


            // if (i * angel > 180)
            // {
            //     objetcs[i].transform.Rotate(new Vector3(0,180,0),Space.Self);objetcs[i].transform.Rotate(new Vector3(0,0,180),Space.Self);
            // }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
