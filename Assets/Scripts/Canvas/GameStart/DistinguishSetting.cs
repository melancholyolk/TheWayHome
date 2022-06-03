using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 下拉框控制器
/// </summary>
public class DistinguishSetting : MonoBehaviour
{
    public Toggle Toggle;

    private void Start()
    {
        OnChange();
    }

    public void OnChange()
    {
        int value = GetComponent<Dropdown>().value;
        bool fullscreen = Toggle.isOn;
        print(value);
        switch (value)
        {
            case 0:
                Screen.SetResolution(2560, 1440, fullscreen);
                break;
            case 1:
                Screen.SetResolution(1960, 1080, fullscreen);
                break;
            case 2:
                Screen.SetResolution(1600, 900, fullscreen);
                break;
        }
    }
    
}
