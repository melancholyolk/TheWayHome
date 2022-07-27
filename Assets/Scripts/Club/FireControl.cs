using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制火焰
/// </summary>
public class FireControl : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(45,-45,0));
    }
}
