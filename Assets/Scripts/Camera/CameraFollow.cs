using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public void SetPlayer(Transform p)
    {
        player = p;
    }

    private void Update()
    {
        if(player != null)
        {
	        // this.transform.position = player.TransformPoint(offset);
	        var position = player.position;
            this.transform.position = position + offset;
        }
    }
}
