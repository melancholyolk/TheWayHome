using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSnowParticle : MonoBehaviour
{
	public AudioSource wind;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player"&&SnowVFXManager.IsAllClose)
        {
            var manager = GameObject.Find("SnowVFXManager").GetComponent<SnowVFXManager>();
            manager.OpenAll();
            SnowVFXManager.IsAllClose = false;
            wind.volume = 1;
        }
    }
}
