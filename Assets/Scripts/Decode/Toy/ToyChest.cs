
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩具柜（解密类）
/// </summary>
public class ToyChest : SyncItem
{
    public bool canUse = false;

    public SpriteRenderer sprite;
    public GameObject toyManager_prefab;

    private List<int> li_int = new List<int>();
    private GameObject ori;
    private void Start()
    {
        li_int.Add(1);
        li_int.Add(2);
        li_int.Add(3);
        li_int.Add(4);
    }
    private void Update()
    {
        if (canUse && !isUsing && !isComplete && OperationControl.Instance.CanOperate() && Input.GetKeyDown(KeyCode.F))
        {
            if (ori)
                Destroy(ori);
            ori = Instantiate(toyManager_prefab);
            ori.transform.parent = transform;
            ori.GetComponent<ToyManager>().Init(li_int);
            OperationControl.Instance.is_decoding = true;
        }
        else if (ori && isUsing && canUse && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F)))
        {
            Destroy(ori);
            OperationControl.Instance.is_decoding = false;
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
        OperationControl.Instance.is_decoding = false;
        isComplete = true;
    }
}
