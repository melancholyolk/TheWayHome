using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CallBack_SnowGroundElectricMachine : DecodeCallBack
{
    public GameObject[] lights;

    public Sprite lightOn;
    public Sprite lightOff;
    public override void CallBack()
    {
        base.CallBack();
        CmdOpenLights();
    }

    [Command(requiresAuthority = false)]
    void CmdOpenLights()
    {
        RpcOpenLights();
    }

    [ClientRpc]
    void RpcOpenLights()
    {
        foreach (var light in lights)
        {
            light.GetComponent<SpriteRenderer>().sprite = lightOn;
            light.GetComponentInChildren<Light>().enabled = true;
        }
        GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>().player.GetComponent<PlayerTest>().AddCondition("电力");
        GetComponent<AudioSource>().Play();
    }
}
