using System.Collections;
using System.Collections.Generic;
using Decode;
using UnityEngine;
/// <summary>
/// 摆放玩具（解密）
/// </summary>
public class ToyManager : DecodeBaseInput
{
	public Sprite[] sprites;
    public Transform[] toy_point;
    public GameObject toy_prefab;
    private List<int> seek_out = new List<int>();
    private List<ToyControl> toys = new List<ToyControl>();
    public void Init(List<int> info)
    {
        seek_out = info;
        for(int i = 0; i < seek_out.Count; i++)
        {
            ToyControl obj = Instantiate(toy_prefab,this.transform).GetComponent<ToyControl>();
            toys.Add(obj);
            obj.tag = seek_out[i];
            obj.Init(toy_point[i+4].gameObject,this,sprites[seek_out[i]-1]);
        }
    }
    private void Clear()
    {
        for(int i = 0; i < toys.Count; i++)
        {
            toys[i].Clear();
        }
        toys.Clear();
        Init(seek_out);
    }

    public void Complete()
    {
        for (int i = 0; i < toys.Count; i++)
        {
            toys[i].is_complete = true;
        }
        DoActions();
    }
}
