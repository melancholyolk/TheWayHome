using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Test1 : NetworkBehaviour
{
    [SyncVar]
    private PlayerInfo player1_info = new PlayerInfo();
    [SyncVar]
    private PlayerInfo player2_info = new PlayerInfo();

    public PlayerInfo cur_info = new PlayerInfo();
    public int cur_num;
    public void SetNum(int num)
    {
        cur_num = num;
    }

    public void Num()
    {
        cur_info.test++;
        SetInfo(cur_info);
    }

    public bool CanPick()
    {
        if (cur_info.prop_panel[0] != null)
        {
            return true;
        }
        else if (cur_info.prop_panel[1] != null)
        {
            return true;
        }
        print("满了");
        return false;
    }

    public void SetPropPanel(PropInfo i)
    {
        if (cur_info.prop_panel[0] != null)
        {
            cur_info.prop_panel[0] = i;
        }
        else if (cur_info.prop_panel[1] != null)
        {
            cur_info.prop_panel[1] = i;
        }
        SetInfo(cur_info);
    }
    private void SetInfo(PlayerInfo info)
    {
        if (cur_num == 1)
        {
            player1_info = (PlayerInfo)info.Clone();
            CmdPlayer1Info(player1_info);
            print("Player1" + player1_info.prop_panel[0] + "   " + player1_info.prop_panel[1]);
            print("Player2" + player2_info.prop_panel[0] + "   " + player2_info.prop_panel[1]);
        }
        else if (cur_num == 2)
        {
            player2_info = (PlayerInfo)info.Clone();
            CmdPlayer2Info(player2_info);
            print("Player1" + player1_info.prop_panel[0] + "   " + player1_info.prop_panel[1]);
            print("Player2" + player2_info.prop_panel[0] + "   " + player2_info.prop_panel[1]);
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdPlayer1Info(PlayerInfo info)
    {
        player1_info = info;
    }




    [Command(requiresAuthority = false)]
    private void CmdPlayer2Info(PlayerInfo info)
    {
        player2_info = info;
    }
}
