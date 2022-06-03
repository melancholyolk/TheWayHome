using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public void SetPlayer(Transform p)
    {
        player = p;
    }

    private void Update()
    {
        if(player != null)
        {
            this.transform.position = new Vector3(player.position.x+ 25.0f, player.position.y + 50.0f, player.position.z - 25.0f);
        }
    }
}
