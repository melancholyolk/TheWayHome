using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SyncItem : NetworkBehaviour
{
    public PlayerMove player;
    [SyncVar]
    public bool isUsing = false;
    [SyncVar]
    public bool isComplete = false;
}
