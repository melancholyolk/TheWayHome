using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
/// <summary>
/// 解密成功后回调函数类
/// </summary>
public class DecodeCallBack : NetworkBehaviour
{
    public virtual void CallBack()
    {
        print("解密成功");
    }
}
