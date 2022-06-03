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
            other.transform.GetChild(2).GetComponent<Light>().intensity = 0.7f;
        }
    }
}
