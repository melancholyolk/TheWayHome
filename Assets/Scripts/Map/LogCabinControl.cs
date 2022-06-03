using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LogCabinControl : MonoBehaviour
{
    public List<SpriteRenderer> hide_wall;
    public List<SpriteRenderer> show_wall;
    public List<SpriteRenderer> show_room_obj;
    public List<SpriteRenderer> hide_room_obj;

    private void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            for (int i = 0; i < hide_wall.Count; i++)
            {
                hide_wall[i].color = new Color(1, 1, 1, 0.4f);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            for (int i = 0; i < hide_wall.Count; i++)
            {
                hide_wall[i].color = new Color(1, 1, 1, 1);
            }
        }
    }
}
