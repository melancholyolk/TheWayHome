using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 传送阵
/// </summary>
public class TransformArea : MonoBehaviour
{
    public Transform tar;

    private GameObject _player;
    public void TransformToTar()
    {
        if (_player)
        {
            float height =  _player.transform.position.y - transform.position.y + 1;
            _player.transform.position = tar.position + Vector3.up * height;
            GameObject.FindObjectOfType<TaskManager>().InitTaskLoader(1);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            _player = other.gameObject;
        }
    }
}
