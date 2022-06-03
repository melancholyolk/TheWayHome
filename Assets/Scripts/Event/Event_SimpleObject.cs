using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 简单物品事件模板
/// 需要满足事件条件才能开启解密
/// </summary>
public class Event_SimpleObject : Event
{
    protected GameObject canvas;
    protected DecodeObjectControl _decode;

    public void Start()
    {
        canvas = GameObject.Find("Canvas");
        _decode = GetComponent<DecodeObjectControl>();
        _decode.enabled = false;
        eventType = EventType.EventObject;
    }

    public void Update()
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

    protected void EventOver()
    {
        if (_triggerDialog.Count > 0)
        {
            canvas.GetComponent<View_Control>().ShowDialog(_triggerDialog);
            CmdEventOver();
            if (GetComponent<DecodeObjectControl>().pre == null)
            {
                SendMessage("Complete");
            }
        }
    }

    [Command(requiresAuthority = false)]
    void CmdEventOver()
    {
        RpcEventOver();
    }

    [ClientRpc]
    void RpcEventOver()
    {
        _decode.enabled = true;
        this.enabled = false;
    }
}