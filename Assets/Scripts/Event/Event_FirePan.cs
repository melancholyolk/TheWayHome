using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_FirePan : Event
{
    [SerializeField] private GameObject torch;
    protected GameObject canvas;
    protected ParticleSystem _fire;

    protected void Start()
    {
        canvas = GameObject.Find("Canvas");
        _fire = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanvasManager.Instance.CanOperate())
        {
            if (canUse)
            {
                if (JudgeCondition())
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
        }
    }

    private void EventOver()
    {
        if (_triggerDialog.Count > 0)
        {
            canvas.GetComponent<View_Control>().ShowDialog(_triggerDialog);
            // Debug.LogWarning(torch);
            CanvasManager.Instance.player.GetComponent<PlayerMove>().CmdHoldObject(0);
        }

        if (eventType == EventType.TaskObject)
        {
            SendTaskManager();
        }
    }

    private void SendTaskManager()
    {
        GameObject.Find("TaskLoader").SendMessage("CmdCompleteTask", _taskMessage);
        CanvasManager.Instance.RemovePropInfo("木棒");
    }
}