using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskLoad : MonoBehaviour
{
    private List<TaskInfo> task_infos = new List<TaskInfo>();

    public TextAsset[] m_propText;
    
    public List<TaskInfo> FindTextByName(string[] text_names)
    {
        List<TextAsset> text_list = new List<TextAsset>();

        foreach (string text_name in text_names)
        {
            var text = System.Array.Find(m_propText, x => x.name == text_name);

            if (text != null)
            {
                text_list.Add(text);
            }
            else
            {
                print("找不到此文件");
            }
        }

        return ReadCSV_StagingDirection(text_list.ToArray());
    }
    public List<TaskInfo> ReadCSV_StagingDirection(TextAsset[] texts)
    {
        List<string> linesOfAllFiles = new List<string>();

        for (int i = 0; i < texts.Length; i++)
        {
            //去除空项
            string[] lines = texts[i].text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            linesOfAllFiles.AddRange(lines);
        }
        return ReadPropText(linesOfAllFiles.ToArray());
    }

    private List<TaskInfo> ReadPropText(string[] lines)
    {
        TaskInfo task_info = new TaskInfo();
        foreach (string line in lines)
        {
            //数据处理  去除空格和注释
            int index = line.IndexOf(";;");
            string str = index < 0 ? line : line.Substring(0, index);
            str = str.Trim().ToLower();
            char[] separator = {' ','\t'};
            string[] division = str.Split(separator);
            switch (division[0])
            {
                case "begin":
                {
                    task_info = new TaskInfo();
                    break;
                }
                case "number":
                {
                    task_info.taskNumber = division[1];
                    break;
                }
                case "target":
                {
                    task_info.taskTarget = division[1];
                    break;
                }
                case "condition":
                {
                    TaskInfo.TaskCondition taskCondition = new TaskInfo.TaskCondition();
                    taskCondition.name = division[1];
                    taskCondition.condition = division[2];
                    task_info.taskCondition.Add(taskCondition);
                    break;
                }
                case "setcondition":
                {
                    TaskInfo.TaskReward taskReward = new TaskInfo.TaskReward();
                    taskReward.name = division[1];
                    taskReward.reward = division[2];
                    task_info.taskReward.Add(taskReward);
                    break;
                }
                case "content":
                {
                    TaskInfo.TaskContent taskContent = new TaskInfo.TaskContent();
                    taskContent.name = division[1];
                    taskContent.content = division[2];
                    task_info.taskContent.Add(taskContent);
                    break;
                }
                case "dialog":
                {
                    task_info.taskDialog.Add(division[1]);
                    break;
                }
                case "end":
                {
                    task_infos.Add(task_info);
                    break;
                }
            }
        }
        return task_infos;
    }
}
