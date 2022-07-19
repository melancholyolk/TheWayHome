using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具界面显示控制
/// </summary>
public class PropPanel : MonoBehaviour
{
    public Animator panelAni;
    private bool is_show = false;

    public PropOperation prop_panel1;
    public PropOperation prop_panel2;
    // Update is called once per frame
    public void ChangeState()
    {
        if(!is_show)
        {
            panelAni.Play("Appear",0,0);
            is_show = true;
        }
        else
        {
            panelAni.Play("Disappear", 0, 0);
            is_show = false;
        }
    }

    public void SetPanel(PropInfo info)
    {
        if (prop_panel1.can_use)
        {
            prop_panel1.SetPanelInfo(info);
        }
        else if(prop_panel2.can_use)
        {
            prop_panel2.SetPanelInfo(info);
        }
    }

    public void RemovePanel(int id)
    {
        if (prop_panel1.prop_info.prop_id == id)
        {
            prop_panel1.InitPanel();
        }
        else if(prop_panel2.prop_info.prop_id == id)
        {
            prop_panel2.InitPanel();
        }
    }
}