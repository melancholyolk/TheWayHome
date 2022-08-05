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
    public GameSetting setting;

    public void SetDistinguish()
    {
	    var solution = setting.distinguish;
	    var size = GetSize(solution);
	    Screen.SetResolution(size.x,size.y,setting.isFullScreen);

	    Toggle.isOn = setting.isFullScreen;
	    GetComponent<Dropdown>().value = solution;
    }
    private void Start()
    {
        OnChange();
    }

    private Vector2Int GetSize(int v)
    {
	    Vector2Int size = new Vector2Int();
	    switch (v)
	    {
		    case 0:
			    size.Set(2560,1440);
			    break;
		    case 1:
			    size .Set(1960,1080);
			    break;
		    case 2:
			    size.Set(1600,900);
			    break;
	    }

	    return size;
    }
    public void OnChange()
    {
        int value = GetComponent<Dropdown>().value;
        bool fullscreen = Toggle.isOn;
        var size = GetSize(value);
        Screen.SetResolution(size.x,size.y,fullscreen);
        //序列化
        setting.distinguish = value;
        setting.isFullScreen = fullscreen;
    }
    
}
