
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PropProperty : NetworkBehaviour
{
    public enum PropType
    {
        None,
        Clue,
        Tool
    }
    [Header("?????څ")]
    public PropType type = PropType.None;

    public PlayerMove player;
    [SyncVar]
    public bool is_pick = false;

    public int num = 0;
    
}
