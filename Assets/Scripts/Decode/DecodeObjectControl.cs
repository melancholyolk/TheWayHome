using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 解密物体
/// 挂在场景中的物体上
/// </summary>

public class DecodeObjectControl : SyncDecodeInfo
{
    public bool canUse = false;

    public GameObject pre;
    
    private GameObject _ori;

    private CanvasManager canvasManager;

    public List<int> prop_num = new List<int>();

    
    private void Start()
    {
        canvasManager = GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>();

    }
    private void Update()
    {
        if (canUse && !isUsing && !isComplete && canvasManager.CanOperate() && Input.GetKeyDown(KeyCode.F))
        {
            if (_ori)
                Destroy(_ori);
            if (!GetComponent<DeCode>() && pre == null)
            {
                Complete();
                return;
            }
            isUsing = true;
            canvasManager.is_decoding = true;
            _ori = Instantiate(pre,transform);
            _ori.transform.position =
                GameObject.FindWithTag("DecodeCamera").transform.position + Vector3.forward * 60 + pre.transform.position;
            _ori.transform.LookAt(GameObject.FindWithTag("DecodeCamera").transform.position);
        }
        else if (isUsing && canUse && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F)))
        {
            Destroy(_ori);
            isUsing = false;
            canvasManager.is_decoding = false;
        }
    }

    public void PlayerNear()
    {
        canUse = true;
    }

    public void PlayerLeave()
    {
        canUse = false;
        Destroy(_ori);
        isUsing = false;
    }

    void Complete()
    {
        isComplete = true;
        canvasManager.is_decoding = false;
        if(prop_num.Count > 0)
        {
            for(int i = 0; i < prop_num.Count; i++)
            {
                GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>().PickUpStart(prop_num[i], 1);
            }
        }

        if (prop_num.Count == 0 && GetComponent<DecodeCallBack>())
        {
            Complete(GetComponent<DecodeCallBack>());
        }
       
    }

    void Complete(DecodeCallBack callBack)
    {
        callBack.CallBack();
    }
}