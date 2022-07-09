using System;
using System.Collections;
using System.Collections.Generic;
using Decode;
using UnityEngine;

public class Event_Sofa : Event
{
    public float stayTime;

    private float _timer = 0;

    private GameObject canvas;

    private ObtainItems _decode;
    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        _decode = GetComponent<ObtainItems>();
        // _decode.enabled = false;
    }

    private void Update()
    {
        if (canUse)
        {
            _timer += Time.deltaTime;
            if (JudgeCondition() && _timer > stayTime)
            {
                EventOver();
            }
            else
            {
                if (_notTriggerDialog.Count > 0)
                {
                    canvas.GetComponent<View_Control>().ShowDialog(_notTriggerDialog);
                    return;
                }
            }
        }
        else
        {
            _timer = 0;
        }
    }
    
    
    private void EventOver()
    {
        if (_triggerDialog.Count > 0)
        {
            canvas.GetComponent<View_Control>().ShowDialog(_triggerDialog);
            // _decode.enabled = true;
            this.enabled = false;
        }
    }
}