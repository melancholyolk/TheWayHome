using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 设置按钮
/// </summary>
public class Button_Setting : MonoBehaviour
{
    public Setting settingPanel;
    private void Start()
    {
        settingPanel = GameObject.Find("Setting").GetComponent<Setting>();
    }

    public void OnClick()
    {
        settingPanel.ShowSetting();
    }
}
