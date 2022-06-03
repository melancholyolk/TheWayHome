using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Altar : Event
{
    protected GameObject canvas;

    public Event_Altar_FirePan[] fireStack;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
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
            print("传送");
        }

        if (eventType == EventType.TaskObject)
        {
            SendTaskManager();
        }
    }

    

    private void SendTaskManager()
    {
        GameObject.Find("TaskLoader").SendMessage("CompleteTask", _taskMessage);
        SendMessage("TransformToTar");
    }

    Stack<int> order = new Stack<int>();
    public void CheckFire(int o)
    {
        if (order.Count < 2) order.Push(o);
        else
        {
            order.Push(o);
            while (order.Count > 1)
            {
                int v = order.Pop();
                if (v <= order.Peek())
                {
                    foreach (var VARIABLE in fireStack)
                    {
                        VARIABLE.TurnOff();
                    }
                    canvas.GetComponent<View_Control>().ShowDialog(new List<string>(new []{"......","好像哪里不对"}));
                    order.Clear();
                    return;
                }
            }
            canvas.GetComponent<CanvasManager>().player.SendMessage("AddCondition", "祭坛");
            canvas.GetComponent<View_Control>().ShowDialog(new List<string>(new []{"祭坛好像被激活了"}));
            order.Clear();
        }
    }
}