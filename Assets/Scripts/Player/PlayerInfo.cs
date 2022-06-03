using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家信息
/// </summary>
public class PlayerInfo : ICloneable
{
    public int test = 0;

    public List<int> clues;

    public List<string> conditions;

    public PropInfo[] prop_panel = new PropInfo[2];

    public object Clone()
    {
        PlayerInfo newDc = new PlayerInfo();
        newDc.test = test;
        newDc.clues = clues;
        newDc.conditions = conditions;
        newDc.prop_panel = prop_panel;
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
            writer.WriteList<string>(player_info.conditions);
            writer.WriteArray<PropInfo>(player_info.prop_panel);
        }

    }

    public static PlayerInfo OnDeserialize(this NetworkReader reader)
    {
        return new PlayerInfo
        {
            test = reader.ReadInt(),
            clues = reader.ReadList<int>(),
            conditions = reader.ReadList<string>(),
            prop_panel = reader.ReadArray<PropInfo>()
        };
    }
}