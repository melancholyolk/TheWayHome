using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// 管理玩家任务信息
/// 挂在当前玩家
/// 判断当前任务是否完成
/// 向UI中加入新的任务
/// 从UI中删去完成的任务
/// </summary>
public class TaskManager : NetworkBehaviour
{
    public TodoListManager listManager;
    public View_Control viewControl;
    public TextAsset[] taskFileName_Player1;
    public TextAsset[] taskFileName_Player2;
    [SerializeField] private TaskLoad _load;
    [SerializeField] private List<TaskUnit> _taskInfos = new List<TaskUnit>();
    [SerializeField] private List<string> _isCompleteIndex = new List<string>();
    [SerializeField] private List<string> _unCompleteIndex = new List<string>();


    private void Start()
    {
        _load = GetComponent<TaskLoad>();
    }

    public void InitTaskLoader(int role)
    {
        string[] names = null;
        if (role == 1)
        {
            names = new string[taskFileName_Player1.Length];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = taskFileName_Player1[i].name;
            }
        }
        else
        {
            names = new string[taskFileName_Player2.Length];
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = taskFileName_Player2[i].name;
            }
        }

        var load = _load.FindTextByName(names);
        foreach (var VARIABLE in load)
        {
            TaskUnit unit = new TaskUnit();
            unit.taskInfo = VARIABLE;
            unit.timer = 0;
            unit.operatable = false;
            _taskInfos.Add(unit);
        }

        CheckNewTask();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Return))
        // {
        //     CmdCompleteTask(_unCompleteIndex[0]);
        // }
    }

    public void CompleteTask(string taskNumber)
    {
        if (_unCompleteIndex.Contains(taskNumber))
        {
            _unCompleteIndex.Remove(taskNumber);
            _isCompleteIndex.Add(taskNumber);
            CheckNewTask();
            listManager.TodoIsDone(taskNumber);
        }

        else
        {
            var index = GetIndexFromTaskNumber(taskNumber);
            if (index != -1)
            {
                var task = _taskInfos[index];
                task.taskInfo.isReady = task.taskInfo.isDone = true;
                _isCompleteIndex.Add(taskNumber);
                CheckNewTask();
            }
        }

        SetTaskReward(taskNumber);
    }

    public void ShowTask(int index)
    {
        if (index >= _taskInfos.Count) return;
        var info = _taskInfos[index].taskInfo;
        if (_unCompleteIndex.Contains(info.taskNumber) || _isCompleteIndex.Contains(info.taskNumber)) return;
        info.isReady = true;
        viewControl.ShowDialog(info.taskDialog);
        StartCoroutine(listManager.CreatTodoUnit(info.taskContent[0].content, info.taskNumber));
        _unCompleteIndex.Add(info.taskNumber);
    }

    public void ShowTask(TaskInfo task)
    {
        var info = task;
        if (_unCompleteIndex.Contains(info.taskNumber) || _isCompleteIndex.Contains(info.taskNumber)) return;
        info.isReady = true;
        viewControl.ShowDialog(info.taskDialog);
        StartCoroutine(listManager.CreatTodoUnit(info.taskContent[0].content, info.taskNumber));
        _unCompleteIndex.Add(info.taskNumber);
    }

    public void CheckNewTask()
    {
        for (int i = 0; i < _taskInfos.Count; i++)
        {
            var info = _taskInfos[i];
            if (info.taskInfo.isDone) continue;
            if (info.taskInfo.isReady) continue;
            bool isReady = false;
            foreach (var content in info.taskInfo.taskCondition)
            {
                if (String.IsNullOrEmpty(content.condition))
                {
                    ShowTask(info.taskInfo);
                    break;
                }
                else
                {
                    if (CheckCondition(info, content.condition)) isReady = true;
                }
            }

            if (isReady)
            {
                ShowTask(info.taskInfo);
            }
        }
    }

    /// <summary>
    /// check wether satify conditions
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    private bool CheckCondition(TaskUnit unit, string conditions)
    {
        string regex = @"^(\w{0,10}(>|<|=)\d{1,10})+$";
        string[] or = conditions.Split(new[] {"||"}, StringSplitOptions.RemoveEmptyEntries);
        foreach (var each in or)
        {
            if (_isCompleteIndex.Contains(each)) return true;
            else
            {
                if (Regex.IsMatch(each, regex))
                {
                    string[] param = Regex.Split(each, @"([><=])");
                    float num = float.Parse(param[2]);
                    switch (param[0])
                    {
                        case "time":
                            if (unit.timer == 0f)
                                StartCoroutine(StartTimer(unit, new[] {num - 1, num + 1}));
                            switch (param[1])
                            {
                                case "<":
                                    if (unit.timer < num)
                                    {
                                        StopCoroutine(StartTimer(unit, new[] {num - 1, num + 1}));
                                        unit.timer = 0f;
                                        return true;
                                    }

                                    break;
                                case ">":
                                    if (unit.timer > num)
                                    {
                                        StopCoroutine(StartTimer(unit, new[] {num - 1, num + 1}));
                                        unit.timer = 0f;
                                        return true;
                                    }

                                    break;
                                case "=":
                                    if (Mathf.Abs(unit.timer - num) < 0.001f)
                                    {
                                        StopCoroutine(StartTimer(unit, new[] {num - 1, num + 1}));
                                        unit.timer = 0f;
                                        return true;
                                    }

                                    break;
                            }

                            break;
                    }
                }

                //fenjie yu
                foreach (var a in or)
                {
                    string[] and = a.Split(new[] {"&&"}, StringSplitOptions.RemoveEmptyEntries);
                    bool ret = true;
                    if (and.Length == 1) break;
                    foreach (var b in and)
                    {
                        if (!_isCompleteIndex.Contains(b))
                        {
                            if (Regex.IsMatch(b, regex))
                            {
                                string[] param = Regex.Split(b, @"([><=])");
                                float num = float.Parse(param[2]);
                                switch (param[0])
                                {
                                    case "time":
                                        if (unit.timer == 0f)
                                            StartCoroutine(StartTimer(unit, new[] {num, num + 1}));
                                        switch (param[1])
                                        {
                                            case "<":
                                                if (unit.timer >= num) ret = false;
                                                break;
                                            case ">":
                                                if (unit.timer <= num) ret = false;
                                                break;
                                            case "=":
                                                if (Mathf.Abs(unit.timer - num) > 0.001f) ret = false;
                                                break;
                                        }

                                        break;
                                }

                                if (!ret)
                                {
                                    StopCoroutine(StartTimer(unit, new[] {num, num + 1}));
                                    break;
                                }
                            }
                            else
                            {
                                ret = false;
                                break;
                            }
                        }
                    }

                    if (ret) return true;
                }
            }
        }

        return false;
    }

    private IEnumerator StartTimer(TaskUnit unit, float[] toggles)
    {
        int i = 0;
        int index = GetIndexFromTaskNumber(unit.taskInfo.taskNumber);
        while (true)
        {
            var unity = _taskInfos[index];
            if (unity.taskInfo.isReady) yield break;
            if (Mathf.Abs(toggles[i] - unity.timer) <= 0.1f)
            {
                CheckNewTask();
                i++;
                if (i >= toggles.Length) yield break;
            }

            unity.timer += Time.deltaTime;
            _taskInfos[index] = unity;
            yield return Time.deltaTime;
        }
    }

    private int GetIndexFromTaskNumber(string number)
    {
        int i = 0;
        for (i = 0; i < _taskInfos.Count; i++)
        {
            if (_taskInfos[i].taskInfo.taskNumber == number)
                return i;
        }

        return -1;
    }

    private void SetTaskReward(string number)
    {
        int index = GetIndexFromTaskNumber(number);
        try
        {
            var reward = _taskInfos[index].taskInfo.taskReward;
            if (reward != null)
            {
                CanvasManager.Instance.player
                    .SendMessage("AddCondition", reward[0].reward);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void ReadTaskLists(out List<string> uncomplete, out List<string> complete)
    {
        uncomplete = new List<string>(_unCompleteIndex);
        complete = new List<string>(_isCompleteIndex);
    }

    public void WriteTaskList(List<string> uncomplete, List<string> complete)
    {
        foreach (string task in uncomplete)
        {
            int index = GetIndexFromTaskNumber(task);
            if (index >= 0)
            {
                ShowTask(index);
            }
        }

        foreach (string task in complete)
        {
            CompleteTask(task);
        }
    }

    private struct TaskUnit
    {
        public TaskInfo taskInfo;
        public float timer;
        public bool operatable;
    }

    [Command(requiresAuthority = false)]
    public void CmdSyncTask(List<string> uncomplete, List<string> complete)
    {
        RpcSyncTask(uncomplete, complete);
    }

    [ClientRpc]
    public void RpcSyncTask(List<string> uncomplete, List<string> complete)
    {
        WriteTaskList(uncomplete, complete);
    }

    [Command(requiresAuthority = false)]
    public void CmdCompleteTask(string taskNumber)
    {
        RpcCompleteTask(taskNumber);
    }

    [ClientRpc]
    public void RpcCompleteTask(string taskNumber)
    {
        CompleteTask(taskNumber);
    }
}