using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// 解谜类
/// 挂在揭秘物体上
/// 判断答案是否正确
/// 进入该类的参数都将可以转化为float形式进行比较
///字母转换为数字 a-z => 0-25 A-Z => 26-51
/// 
/// </summary>
public class DeCode : MonoBehaviour
{
    public string answer;
    public float error;
    private float[] m_Answers;


    private void Start()
    {
        Time.timeScale = 1;
        ProcessAnswer();
    }

    public void ProcessAnswer()
    {
        //输入处理
        string[] s_parameters = answer.Split(';');
        m_Answers = new float[s_parameters.Length];
        for (int i = 0; i < s_parameters.Length; i++)
        {
            if (s_parameters[i] == "" || s_parameters[i] == null)
            {
                m_Answers[i] = 0;
                continue;
            }

            m_Answers[i] = float.Parse(s_parameters[i]);
        }
    }

    public bool Check(string input)
    {
        print(input);
        //输入处理
        string[] s_parameters = input.Split(';');
        if (s_parameters.Length != m_Answers.Length)
        {
            print("输入不正确");
            return false;
        }

        float[] f_parameters = new float[s_parameters.Length];
        for (int i = 0; i < s_parameters.Length; i++)
        {
            if (s_parameters[i] == "" || s_parameters[i] == null)
            {
                f_parameters[i] = 0;
                continue;
            }

            f_parameters[i] = float.Parse(s_parameters[i]);
        }

        for (int j = 0; j < f_parameters.Length; j++)
        {
            if (Mathf.Abs(f_parameters[j] - m_Answers[j]) > error) break;
            if (j == f_parameters.Length - 1)
            {
                print("答案正确");
                SendMessage("Complete");
                return true;
            }
        }

        print("答案错误");
        return false;
    }
}