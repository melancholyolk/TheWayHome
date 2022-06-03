
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÕÊæﬂπÒ£®Ω‚√‹¿‡£©
/// </summary>
public class ToyChest : SyncDecodeInfo
{
    public bool canUse = false;

    public SpriteRenderer sprite;
    public GameObject toyManager_prefab;

    private List<int> li_int = new List<int>();
    private GameObject ori;

    private CanvasManager canvasManager;
    private void Start()
    {
        canvasManager = GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>();
        li_int.Add(1);
        li_int.Add(2);
        li_int.Add(3);
        li_int.Add(4);
    }
    private void Update()
    {
        if (canUse && !isUsing && !isComplete && canvasManager.CanOperate() && Input.GetKeyDown(KeyCode.F))
        {
            if (ori)
                Destroy(ori);
            ori = Instantiate(toyManager_prefab);
            ori.transform.parent = transform;
            ori.GetComponent<ToyManager>().Init(li_int);
            player.CmdDecodeIsUse(this, true);
            canvasManager.is_decoding = true;
        }
        else if (ori && isUsing && canUse && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F)))
        {
            Destroy(ori);
            player.CmdDecodeIsUse(this, false);
            canvasManager.is_decoding = false;
        }
    }
    void PlayerNear()
    {
        canUse = true;
        //sprite.color = new Color(1, 1, 1, 1f);
    }

    void PlayerLeave()
    {
        canUse = false;
        //sprite.color = new Color(1, 1, 1, 0f);
        if (ori)
        {
            Destroy(ori);
            isUsing = false;
        }


    }

    void Complete()
    {
        canvasManager.is_decoding = false;
        isComplete = true;
    }
}
