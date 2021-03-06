using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderQueueControl : MonoBehaviour
{
    [Range(2500,3000)]
    public int order;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Material material = other.GetComponent<PlayerMove>().playerMaterial;
            material.renderQueue = order;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Material material = other.GetComponent<PlayerMove>().playerMaterial;
            material.renderQueue = 3000;
        }
    }
}
