using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(PropJudgePlayer))]
public class PropPick : PropProperty
{
    [Space] public bool canUse = false;

    public SpriteRenderer sprite;
    public CanvasManager canvasManager;
    private SyncManager syncManager;
    private PropInfo propInfo;

    [ContextMenu("GetInfo")]
    private void SetInfo()
    {
        syncManager = GameObject.FindObjectOfType<SyncManager>();
        if (num != 0)
        {
            propInfo = syncManager.GetPropInfoByNum(num);
            sprite.sprite = propInfo.prop_sprite;
            this.transform.name = propInfo.prop_name;
        }
    }

    public void SetPropInfo(int num1)
    {
        syncManager = GameObject.FindObjectOfType<SyncManager>();
        PropInfo info = syncManager.GetPropInfoByNum(num1);
        propInfo = info;
        sprite.sprite = info.prop_sprite;
        num = int.Parse(info.prop_number);
        this.transform.name = info.prop_name;
    }

    private void Start()
    {
        SetInfo();
        canvasManager = GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canvasManager.CanOperate() && canvasManager.CanPick())
        {
            if (canUse && !is_pick)
            {
                player.GetComponent<PlayerTest>().CmdDestory(this.transform.name);
                canvasManager.PickUpStart(num, 1);
            }
        }

        if (is_pick)
        {
            Destroy(this.gameObject);
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