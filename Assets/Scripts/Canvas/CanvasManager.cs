using System;
using System.Collections;
using System.Collections.Generic;
using Decode;
using Scriptable;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
	public static CanvasManager Instance;

    private bool cluemenu_show = false;
    public PickUpShow pickUpShow;
    public PropPanel propPanel;
    public GameObject clue_menu;
    public PlayerMove player;
    public Panel_Setting setting;
    public GameInfoSave save;
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
	    save = GameState.currentSave;
	    setting.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && (OperationControl.Instance.CanOperate() || cluemenu_show))
        {
            cluemenu_show = !cluemenu_show;
            OperationControl.Instance.ui_isOpening = cluemenu_show;
            clue_menu.SetActive(cluemenu_show);
            propPanel.ChangeState();
            if (cluemenu_show)
            {
                SyncManager.Instance.ShowClue();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
	        SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
	        LoadData();
        }
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
        OperationControl.Instance.is_picking = true;
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

    private void SaveData()
    {
	    if(player_type == Player.Player1)
			save.playerInfo1 = PlayerManager.Instance.GetPlayerInfo();
	    else
		    save.playerInfo2 = PlayerManager.Instance.GetPlayerInfo();

	    save.isUsing = true;
	    save.obtainItemses = MonoECSInteract.Instance.GetObtainItems();
	    save.process = GameState.state;
    }

    private void LoadData()
    {
	    PlayerInfo info;
	    if (player_type == Player.Player1)
		    info = save.playerInfo1;
	    else
		    info = save.playerInfo2;
	    MonoECSInteract.Instance.SetObtainItems(save.obtainItemses);
	    player.SetCondition(info.conditions);
	    player.transform.position = info.position;
	    
	    save.DoActions();
    }
}