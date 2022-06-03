using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ChoseButton : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnStateChanged))]
    private bool is_chosed = false;
    private SpriteRenderer image;
    [SyncVar(hook = nameof(OnServerChanged))]
    private bool is_server = false;
    
    public ButtonManager btn_mng;

    private void Awake()
    {
        image = GetComponent<SpriteRenderer>();
    }
    private void OnServerChanged(bool oldbool, bool newbool)
    {
        if (is_server)
        {
        }
    }
    private void OnStateChanged(bool oldbool, bool newbool)
    {
        if (is_chosed)
        {
            image.color = new Color(1, 1, 1, 1);
            btn_mng.ButtonDown();
        }
        else
        {
            image.color = new Color(0.5f, 0.5f, 0.5f, 1);
            btn_mng.ButtonUp();
        }
    }
    public void ChangeState(bool temp, bool server)
    {
        is_chosed = temp;
        is_server = server;
    }
    public bool GetState()
    {
        return is_chosed;
    }
}
