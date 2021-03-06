using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
	public static CanvasManager Instance;
    public bool is_picking = false;
    public bool is_setting = false;
    public bool is_decoding = false;
    public bool is_dialog = false;
    public bool cluemenu_show = false;
    public PickUpShow pickUpShow;
    public PropPanel propPanel;
    public GameObject clue_menu;
    public PlayerMove player;

    [Serializable]
    public enum Player
    {
        None,
        Player1 = 1,
        Player2 = 2
    }
    [SerializeField]
    public Player player_type = Player.Player1;

    private void Awake()
    {
	    Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && (CanOperate() || cluemenu_show))
        {
            cluemenu_show = !cluemenu_show;
            clue_menu.SetActive(cluemenu_show);
            propPanel.ChangeState();
            if (cluemenu_show)
            {
                SyncManager.Instance.ShowClue();
            }
        }
    }

    public void SetPickState(bool temp)
    {
        is_picking = temp;
    }

    public void SetSettingState(bool temp)
    {
        is_setting = temp;
    }

    public void SetDecodeState(bool temp)
    {
        is_decoding = temp;
    }

    public bool CanOperate()
    {
        if (!is_decoding && !is_picking && !cluemenu_show && !is_setting)
        {
            return true;
        }

        return false;
    }

    public bool CanPick()
    {
        return PlayerManager.Instance.CanPick();
    }

    public void SetPropPanel(PropInfo info)
    {
        PlayerManager.Instance.SetPropPanel(info);
        propPanel.SetPanel(info);
        player.AddCondition(info.prop_id);
    }

    public void DiscarderProp(PropInfo info)
    {
        player.DiscarderProp(info);
        PlayerManager.Instance.RemovePropPanel(info.prop_id);
    }


    public void RemovePropInfo(int id)
    {
        PlayerManager.Instance.RemovePropPanel(id);
        propPanel.RemovePanel(id);
        player.RemoveCondition(id);
    }
    
    public void PickUpStart(int num, float size)
    {
        PropInfo info = SyncManager.Instance.GetPropInfoByNum(num);
        is_picking = true;
        pickUpShow.PickUpStart(info.prop_sprite , size, info.prop_name,info.prop_describe);
        if (info.prop_type == "t")
        {
            if (PlayerManager.Instance.CanPick())
            {
                SetPropPanel(info);
            }
            else
            {
                player.DiscarderProp(info);
            }
        }else if(info.prop_type == "c")
        {
            SyncManager.Instance.SetOwnClue(num);
            player.AddCondition(info.prop_id);
        }
    }
    
    public void SyncTask()
    {
        List<string> uncomplet;
        List<string> complete;

        GameObject.FindObjectOfType<TaskManager>().ReadTaskLists(out uncomplet, out complete);

        GameObject.FindObjectOfType<TaskManager>().CmdSyncTask(uncomplet, complete);
    }
}