using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInfo : MonoBehaviour
{
    public string taskNumber = "";
    public string taskTarget = "";
    public List<TaskCondition> taskCondition = new List<TaskCondition>();
    public List<TaskContent> taskContent = new List<TaskContent>();
    public List<string> taskDialog = new List<string>();
    public List<TaskReward> taskReward = new List<TaskReward>();
    public bool isDone = false;//True if complete
    public bool isReady = false;//True if show in the tastlist
    public struct TaskCondition
    {
        public string name;
        public string condition;
    }
    
    public struct TaskContent
    {
        public string name;
        public string content;
    }
    public struct TaskReward
    {
        public string name;
        public string reward;
    }
}
