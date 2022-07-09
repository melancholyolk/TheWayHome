using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SyncDecodeInfo : NetworkBehaviour
{
    public PlayerTest player;
    [SyncVar]
    public bool isUsing = false;
    [SyncVar]
    public bool isComplete = false;
}
