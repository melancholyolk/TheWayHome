using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncManager : NetworkBehaviour
{
    private List<PropInfo> prop_info = new List<PropInfo>();
    /// <summary>
    /// 线索管理
    /// </summary>
    private List<int> own_clue = new List<int>();
    [SyncVar(hook = nameof(OnPropAdd))]
    private int num = 0;
    private List<int> public_clue = new List<int>();
    /// <summary>
    /// 对话管理
    /// </summary>
    private int own_dialogIndex = 0;

    public int public_dialogIndex = -1;

    public List<Sprite> sprites;

    public bool is_used =false;

    private Book book;
    private void Start()
    {
        string[] str = {"Prop"};
        prop_info = GetComponent<PropLoad>().FindTextByName(str);

    }

    private void Awake()
    {
        GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>().syncManager = this;
    }

    [Command(requiresAuthority =false)]
    private void CmdAddPublicClue(int temp)
    {
        num = temp;
    }

    private void OnPropAdd(int oldnum, int newnum)
    {
        public_clue.Add(num);
    }

    public void ShowClue()
    {
        book = GameObject.FindWithTag("Book").GetComponent<Book>();
        book.SetPropInfo(GetPropInfo());
    }

    public List<PropInfo> GetPropInfo()
    {
        List<PropInfo> prop_temp = new List<PropInfo>();
        for(int i = 0;i < own_clue.Count; i++)
        {
            prop_temp.Add(GetPropInfoByNum(own_clue[i]));
        }
        for (int i = 0; i < public_clue.Count; i++)
        {
            prop_temp.Add(GetPropInfoByNum(public_clue[i]));
        }
        return prop_temp;
    }

    public PropInfo GetPropInfoByNum(int num)
    {
        string[] str = { "Prop" };
        prop_info = GetComponent<PropLoad>().FindTextByName(str);
        prop_info[num].prop_sprite = sprites[num-1];
        return prop_info[num];
    }

    private PropInfo SetPropInfo(int num)
    {
        prop_info[num].prop_sprite = sprites[num - 1];
        return prop_info[num];
    }

    public void SetOwnClue(int num)
    {
        own_clue.Add(num);
    }
}
