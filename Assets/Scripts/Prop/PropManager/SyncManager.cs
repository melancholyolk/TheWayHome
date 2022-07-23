using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncManager : NetworkBehaviour
{
    public static SyncManager Instance;

    private static List<PropInfo> prop_info = new List<PropInfo>();
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

    [SerializeField]
    public List<Sprite> sprites;

    public bool is_used =false;

    private Book book;
    private void Start()
    {
        Instance = this;
        string[] str = {"Prop"};
        prop_info = GetComponent<PropLoad>().FindTextByName(str);
        for (int i = 1; i < prop_info.Count; i++)
        {
	        prop_info[i].prop_sprite = sprites[i - 1];
	        prop_info[i].prop_id = i;
        }
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
        prop_info[num].prop_sprite = sprites[num - 1];
        prop_info[num].prop_id = num;
        return prop_info[num];
    }

    public PropInfo EditorGetPropInfoByNum(int num)
    {
        string[] str = { "Prop" };
        prop_info = GetComponent<PropLoad>().FindTextByName(str);
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
