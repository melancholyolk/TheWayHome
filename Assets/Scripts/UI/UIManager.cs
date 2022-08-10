using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, BaseUI> ui_map;

    private string cur_ui;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddUI(string str, BaseUI ui)
    {
        ui_map.Add(str,ui);
        cur_ui = str;
    }

    public void ChangeCurrentUI(string str)
    {
        if(ui_map[str].needHideOther)
        {
            ui_map[cur_ui].OnClosed();
		}
        if(ui_map[str].needBlackBg)
        {
            
		}
        ui_map[str].OnViewBack();
    }
}
