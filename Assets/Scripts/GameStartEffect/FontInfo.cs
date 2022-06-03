using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontInfo : MonoBehaviour
{
    public bool waiting;

    private float _initSpeed = 0.8f;

    private void FixedUpdate()
    {
        if (waiting) return;
        transform.Translate(Vector3.forward * _initSpeed);
        _initSpeed += 0.01f;
        if (Vector3.Distance(transform.position, transform.parent.position) > 1000f)
        {
            OnDestroyMe();
        }
    }

    private void OnDestroyMe()
    {
        waiting = true;
        var t = transform.parent.GetComponent<LoadingGame_FontEffect>().current;
        t = Mathf.Clamp(t - 1, 0, 100);
        transform.parent.GetComponent<LoadingGame_FontEffect>().current = t;
        Destroy(gameObject);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Vector3.Distance(transform.position, transform.parent.position) > 30f)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    waiting = true;
                }
            }
        }
        else
        {
            waiting = false;
        }
    }
}