using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSnowParticle : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player"&&!SnowVFXManager.IsAllClose)
        {
            var manager = GameObject.Find("SnowVFXManager").GetComponent<SnowVFXManager>();
            manager.CloseAll();
            SnowVFXManager.IsAllClose = true;
        }
    }
}
