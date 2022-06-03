using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 向棺材加上碰撞体
/// </summary>
public class AddColliderToBox : MonoBehaviour
{
    private GameObject _boxCollider;
    // Start is called before the first frame update
    // void Start()
    // {
    //     BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
    //     _boxCollider = new GameObject("Collider"+transform.name,typeof(BoxCollider));
    //     _boxCollider.transform.position = boxCollider.bounds.center;
    //     _boxCollider.transform.parent = transform;
    //     Destroy(boxCollider);
    // }

    private void OnDrawGizmos()
    {
        if (_boxCollider == null&&(transform.childCount == 0))
        {
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
            _boxCollider = new GameObject("Collider"+transform.name,typeof(BoxCollider));
            // _boxCollider.transform.position = boxCollider.bounds.center;
            var pos = boxCollider.bounds.center;
            pos.x = transform.position.x;
            _boxCollider.transform.position = pos;
            _boxCollider.transform.parent = transform;
            var size = _boxCollider.GetComponent<BoxCollider>().size;
            size.x = boxCollider.size.x * Mathf.Cos(Mathf.PI / 4)/2;
            size.z = size.x;
            size.y = boxCollider.size.y * Mathf.Cos(Mathf.PI / 4)/2;
            _boxCollider.GetComponent<BoxCollider>().size = size;
            DestroyImmediate(boxCollider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
