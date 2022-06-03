using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    public int taskIndex;

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("TaskLoader").SendMessage("ShowTask",taskIndex);
    }
}
