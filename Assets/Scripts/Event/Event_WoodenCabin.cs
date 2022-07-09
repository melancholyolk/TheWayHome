using System.Collections;
using System.Collections.Generic;
using Decode;
using UnityEngine;

public class Event_WoodenCabin : Event
{
    protected GameObject canvas;
    private ObtainItems _obtainItems;
    protected void Start()
    {
        canvas = GameObject.Find("Canvas");
        _obtainItems = GetComponent<ObtainItems>();
        eventType = EventType.TaskObject;

        // _obtainItems.enabled = false;
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
            // _obtainItems.enabled = true;
            GameObject.FindWithTag("Player").SendMessage("RemoveCondition", "灰色钥匙");
            canvas.GetComponent<CanvasManager>().RemovePropInfo("灰色钥匙");   
        }
    }
}
