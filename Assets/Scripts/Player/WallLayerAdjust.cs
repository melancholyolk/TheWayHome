using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 控制玩家与墙的图层关系
/// </summary>
public class WallLayerAdjust : MonoBehaviour
{
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(Player.transform.position + Vector3.up * -1));
        if (Physics.Raycast(ray, out hit,10000,layerMask:13))
        {
            print(hit.collider.name);
            if (hit.collider.tag == "Player")
            {
                print(hit.collider.name);
                var players = Player.GetComponent<PlayerTest>().players;
                foreach (var VARIABLE in players)
                {
                    VARIABLE.GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
            else if (hit.collider.tag == "Wall")
            {
                var players = Player.GetComponent<PlayerTest>().players;
                foreach (var VARIABLE in players)
                {
                    VARIABLE.GetComponent<SortingGroup>().sortingOrder = -1;
                }
            }
        }
    }
}