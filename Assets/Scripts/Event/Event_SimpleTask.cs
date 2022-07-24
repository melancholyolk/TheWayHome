using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_SimpleTask : Event
{
    public string removeTool;

    protected GameObject canvas;

    protected void Start()
    {
        canvas = GameObject.Find("Canvas");
        eventType = EventType.TaskObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && OperationControl.Instance.CanOperate())
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
        }

        SendTaskManager();
    }

    protected virtual void SendTaskManager()
    {
        GameObject.Find("TaskLoader").SendMessage("CmdCompleteTask", _taskMessage);
        if (!string.IsNullOrEmpty(removeTool))
        {
            // CanvasManager.Instance.RemovePropInfo(removeTool);
        }
    }
}