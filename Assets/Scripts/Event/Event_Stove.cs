using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Event_Stove : Event_SimpleTask
{
    private Light _light;

    private void Awake()
    {
        base.Start();
        _light = GetComponentInChildren<Light>();
        _light.enabled = false;
    }

    protected override void SendTaskManager()
    {
        base.SendTaskManager();
        CmdOpenLight();
    }

    [Command(requiresAuthority = false)]
    void CmdOpenLight()
    {
        RpcOpenLight();
    }

    [ClientRpc]
    void RpcOpenLight()
    {
        _light.enabled = true;
    }
}
