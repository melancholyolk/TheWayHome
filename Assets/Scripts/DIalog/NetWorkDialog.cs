using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetWorkDialog : NetworkBehaviour
{
    [SyncVar (hook = nameof(SetIndex))]
    public int currentIndex;
    
    [Command]
    public void CmdOpenDialogView(int index )
    {
        currentIndex = 0;
        RpcOpenDialogView(index);
    }

    [ClientRpc]
    public void RpcOpenDialogView(int index)
    {
        GetComponent<View_Control>().dialogView.ShowView();
    }

    [Command]
    public void CmdLoadNext()
    {
        ++currentIndex;
        print(currentIndex);
        RpcLoadNext();
    }

    [ClientRpc]
    public void RpcLoadNext()
    {
        print("NextSentence"+currentIndex);
        StartCoroutine(DelaySend());
    }

    public void SetIndex(int _,int index)
    {
    }

    IEnumerator DelaySend()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponentInChildren<Dialog_View>().NextTarSentence(currentIndex);
    }
}
