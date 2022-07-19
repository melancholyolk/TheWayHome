using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
/// <summary>
/// 事件触发父类
/// </summary>
public class Event : NetworkBehaviour
{
    public enum EventType
    {
        TaskObject,
        EventObject
    }
    public EventType eventType;
    public List<string> _notTriggerDialog = new List<string>();
    public List<string> _triggerDialog = new List<string>();
    public List<int> _triggerCondition = new List<int>();
    public string _taskMessage = "";

    public GameObject player;
    public bool canUse = false;
    /// <summary>
    /// 状态判断
    /// </summary>
    public bool JudgeCondition()
    {
        foreach (var VARIABLE in _triggerCondition)
        {
	        if(!player.GetComponent<PlayerMove>().JudgeCondition(VARIABLE))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 任务完成
    /// </summary>
    public void TaskComplete()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            player = other.gameObject;
            canUse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            canUse = false;
        }
    }
}
