using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class JudgePlayer : MonoBehaviour
{
    public GameObject player;
    public bool can_use = false;

    private SyncItem propInfo;

    private void Start()
    {
        propInfo = GetComponent<SyncItem>();
    }
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && !propInfo.isUsing)
        {
            can_use = true;
            player = other.gameObject;
            propInfo.player = player.GetComponent<PlayerMove>();
            SendMessage("PlayerNear");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            can_use = false;
            player = other.gameObject;
            SendMessage("PlayerLeave");
        }
    }
}
