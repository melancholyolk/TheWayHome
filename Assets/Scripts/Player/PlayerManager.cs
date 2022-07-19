using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家信息管理
/// </summary>
public class PlayerManager
{
    private static PlayerManager _instance;
    public static PlayerManager Instance { 
        get{
            if(_instance == null)
            {
                _instance = new PlayerManager();
            }
            return _instance;
        }
        set{
	    }
    }
    //[SyncVar]
    //private PlayerInfo player1_info = new PlayerInfo();
    //[SyncVar]
    //private PlayerInfo player2_info = new PlayerInfo();
    public PlayerInfo cur_info = new PlayerInfo();
    public int cur_num;

    private bool panel1_isusing = false;
    private bool panel2_isusing = false;

    public void SetNum(int num)
    {
        cur_num = num;
    }
    public bool CanPick()
    {
        if (!panel1_isusing)
        {
            return true;
        }
        else if (!panel2_isusing)
        {
            return true;
        }

        return false;
    }

    public void SetPropPanel(PropInfo info)
    {
        if (!panel1_isusing)
        {
            cur_info.prop_panel[0] = info;
            panel1_isusing = true;
        }
        else if (!panel2_isusing)
        {
            cur_info.prop_panel[1] = info;
            panel2_isusing = true;
        }
    }

    public void RemovePropPanel(int id)
    {
        if (panel1_isusing && cur_info.prop_panel[0].prop_id == id)
        {
            cur_info.prop_panel[0] = null;
            panel1_isusing = false;
        }
        else if (panel2_isusing &&cur_info.prop_panel[1].prop_id == id)
        {
            cur_info.prop_panel[1] = null;
            panel2_isusing = false;
        }
    }

    //private void SetInfo(PlayerInfo info)
    //{
    //    if (cur_num == 1)
    //    {
    //        player1_info = (PlayerInfo)info.Clone();
    //        CmdPlayer1Info(player1_info);
    //    }
    //    else if (cur_num == 2)
    //    {
    //        player2_info = (PlayerInfo)info.Clone();
    //        CmdPlayer2Info(player2_info);
    //    }
    //}

    //[Command(requiresAuthority = false)]
    //private void CmdPlayer1Info(PlayerInfo info)
    //{
    //    player1_info = info;
    //}

    //[Command(requiresAuthority = false)]
    //private void CmdPlayer2Info(PlayerInfo info)
    //{
    //    player2_info = info;
    //}
}
