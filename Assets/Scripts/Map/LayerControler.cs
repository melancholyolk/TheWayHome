using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LayerControler : MonoBehaviour
{
    public int layerOrder;
    public bool onExit = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"||other.tag =="OtherPlayer")
        {
            var players = other.gameObject.GetComponent<PlayerTest>().players;
            foreach (var VARIABLE in players)
            {
                VARIABLE.GetComponent<SortingGroup>().sortingOrder = layerOrder;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (onExit)
        {
            if (other.tag == "Player" || other.tag == "OtherPlayer")
            {
                var players = other.gameObject.GetComponent<PlayerTest>().players;
                foreach (var VARIABLE in players)
                {
                    VARIABLE.GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
        }
    }
}