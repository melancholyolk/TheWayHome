using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTaskTrigger : MonoBehaviour
{
    bool kick = false;

    private void OnTriggerStay(Collider other)
    {
        if (!kick)
        {
            if(other.tag == "OtherPlayer")
            {
                kick = true;
                GameObject.FindWithTag("Canvas").SendMessage("SyncTask");
            }
        }
    }
}
