using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_WoodenCabin : Event
{
    protected GameObject canvas;
    private DecodeObjectControl _decodeObjectControl;
    protected void Start()
    {
        canvas = GameObject.Find("Canvas");
        _decodeObjectControl = GetComponent<DecodeObjectControl>();
        eventType = EventType.TaskObject;

        _decodeObjectControl.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canvas.GetComponent<CanvasManager>().CanOperate())
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
            _decodeObjectControl.enabled = true;
            GameObject.FindWithTag("Player").SendMessage("RemoveCondition", "灰色钥匙");
            canvas.GetComponent<CanvasManager>().RemovePropInfo("灰色钥匙");   
        }
    }
}
