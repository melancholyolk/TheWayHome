using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTrigger : MonoBehaviour
{
    public string direct;
    public float force;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("direct",direct);
            hashtable.Add("force",force);
            other.SendMessage("ChangeState",hashtable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("direct","");
            hashtable.Add("force",0f);
            other.SendMessage("ChangeState",hashtable);
        }
    }
}
