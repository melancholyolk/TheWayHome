using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 判断玩家是否在道具判定范围内
/// </summary>
[RequireComponent(typeof(Collider))]
public class PropJudgePlayer : MonoBehaviour
{
    public GameObject player;

    private PropProperty propInfo;

    private void Start()
    {
        propInfo = GetComponent<PropProperty>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            SendMessage("PlayerNear");
            player = other.gameObject;
            propInfo.player = player.GetComponent<PlayerMove>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            SendMessage("PlayerLeave");
            player = other.gameObject;
        }
    }
}
