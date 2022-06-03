using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class DoorControl : NetworkBehaviour
{
    public bool is_v = false;
    public AudioClip open;
    private bool is_open = false;

    private bool can_use = false;

    private Animator animator;
    private float time = 0;
    private AudioSource _audioSource;
    public bool is_positive = false;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (can_use && Time.time - time > 1 && !is_open)
            {
                time = Time.time;
                if (is_positive)
                {
                    CmdDoor("open");
                    if (is_v)
                    {
                        GetComponent<SpriteRenderer>().sortingOrder = -1; 
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sortingOrder = 1;
                    }
                }
                else
                {
                    CmdDoor("openother"); 
                    if (is_v)
                    {
                        GetComponent<SpriteRenderer>().sortingOrder = 1; 
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sortingOrder = -1;
                    }
                }

                is_open = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            can_use = true;
            if (is_v)
            {
                if (other.transform.position.x - this.transform.position.x > 0)
                {
                    is_positive = true;
                }
                else
                {
                    is_positive = false;
                }
            }
            else
            {
                if (other.transform.position.z - this.transform.position.z > 0)
                {
                    is_positive = true;
                }
                else
                {
                    is_positive = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            can_use = false;
            if (is_open)
            {
                if(is_positive)
                    CmdDoor("close");
                else
                    CmdDoor("closeother");
            }

            is_open = false;
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdDoor(string name)
    {
        RpcDoor(name);
    }

    [ClientRpc]
    private void RpcDoor(string name)
    {
        animator.SetTrigger(name);
        _audioSource.PlayOneShot(open);
    }
}
