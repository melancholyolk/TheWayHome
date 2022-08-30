using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家信息
/// </summary>
[Serializable]
public class PlayerInfo : ICloneable
{
    public int test = 0;

    public List<int> clues;

    public List<int> conditions;

    public PropInfo[] prop_panel;

    public Vector3 position = new Vector3();

    public PlayerInfo()
    {
	    clues = new List<int>();
	    conditions = new List<int>();
	    prop_panel = new PropInfo[2];
    }
    public object Clone()
    {
        PlayerInfo newDc = new PlayerInfo();
        newDc.test = test;
        clues.CopyTo(newDc.clues);
        conditions.CopyTo(newDc.conditions);
        newDc.prop_panel = prop_panel.Clone() as PropInfo[];
        newDc.position = position;
        return newDc;
    }
}
public static class PlayerInfoSerializer
{
    public static void OnSerialize(this NetworkWriter writer, PlayerInfo info)
    {
        if (info is PlayerInfo player_info)
        {
            writer.WriteInt(player_info.test);
            writer.WriteList<int>(player_info.clues);
            writer.WriteList<int>(player_info.conditions);
            writer.WriteArray<PropInfo>(player_info.prop_panel);
            writer.WriteVector3(player_info.position);
        }

    }

    public static PlayerInfo OnDeserialize(this NetworkReader reader)
    {
        return new PlayerInfo
        {
            test = reader.ReadInt(),
            clues = reader.ReadList<int>(),
            conditions = reader.ReadList<int>(),
            prop_panel = reader.ReadArray<PropInfo>(),
            position = reader.ReadVector3()
        };
    }
}