using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(PropJudgePlayer))]
public class PropPick : PropProperty
{
    [Space] public bool canUse = false;

    public SpriteRenderer spriteRen;
    private SyncManager syncManager;
    private PropInfo propInfo;
    
    [ContextMenu("GetInfo")]
    private void SetInfo()
    {
        spriteRen = GetComponent<SpriteRenderer>();
        syncManager = GameObject.FindObjectOfType<SyncManager>();
        if (num != 0)
        {
            propInfo = syncManager.EditorGetPropInfoByNum(num);
            spriteRen.sprite = propInfo.prop_sprite;
            this.transform.name = propInfo.prop_name;
        }
    }

    public void SetPropInfo(int num1)
    {
        spriteRen = GetComponent<SpriteRenderer>();
        PropInfo info = SyncManager.Instance.GetPropInfoByNum(num1);
        propInfo = info;
        spriteRen.sprite = info.prop_sprite;
        num = int.Parse(info.prop_number);
        this.transform.name = info.prop_name;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && OperationControl.Instance.CanOperate() && CanvasManager.Instance.CanPick())
        {
            if (canUse && !is_pick)
            {
                player.GetComponent<PlayerMove>().CmdDestory(this.transform.name);
                CanvasManager.Instance.PickUpStart(num, 1);
            }
        }

        if (is_pick)
        {
            ObjectPool._instance.RecycleGo(this.gameObject);
        }
    }   
    void PlayerNear()
    {
	    canUse = true;
    }

    void PlayerLeave()
    {
	    canUse = false;
    }
}