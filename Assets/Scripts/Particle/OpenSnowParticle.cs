using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSnowParticle : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player"&&SnowVFXManager.IsAllClose)
        {
            var manager = GameObject.Find("SnowVFXManager").GetComponent<SnowVFXManager>();
            manager.OpenAll();
            SnowVFXManager.IsAllClose = false;
            other.transform.GetChild(2).GetComponent<Light>().intensity = 0.7f;
        }
    }
}
