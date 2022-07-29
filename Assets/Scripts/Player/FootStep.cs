using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FootStep : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] audioClip;
    public AudioSource audioSource;
    private int _groupIndex;
    private int index = 0;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _groupIndex = 0;
    }

    public void Footstep()
    {
        AudioClip clip;
        RaycastHit hit;
        Ray ray = new Ray(transform.position,Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            switch (hit.transform.tag)
            {
                case "SnowGround":
                    _groupIndex = 1;
                    break;
                case "WoodGround":
                    _groupIndex = 2;
                    break;
                default:
                    _groupIndex = 0;
                    break;
            }
        }
        audioSource.PlayOneShot(audioClip[_groupIndex*2+index++%2]);
    }
}
