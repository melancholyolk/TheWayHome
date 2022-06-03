using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInput : MonoBehaviour
{
    protected virtual void SendAnswer()
    {
        
    }

    /// <summary>
    /// 解密成功后调用
    /// </summary>
    protected virtual IEnumerator Success()
    {
        yield return null;
    }
}
