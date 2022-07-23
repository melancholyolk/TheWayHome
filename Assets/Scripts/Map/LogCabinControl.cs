using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LogCabinControl : MonoBehaviour
{
    public List<GameObject> hide_wall;

    private bool m_IsEntered = false;
    // public List<SpriteRenderer> show_room_obj;
    // public List<SpriteRenderer> hide_room_obj;

    private void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player")&&!m_IsEntered)
        {
            for (int i = 0; i < hide_wall.Count; i++)
            {
	            hide_wall[i].GetComponent<MeshRenderer>().enabled = false;
            }
            m_IsEntered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag.Equals("Player")&&m_IsEntered)
        {
            for (int i = 0; i < hide_wall.Count; i++)
            {
	            hide_wall[i].GetComponent<MeshRenderer>().enabled = true;
            }

            m_IsEntered = false;
        }
    }
}
