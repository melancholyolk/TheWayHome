using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public GameObject bkPanel;
    public GameObject settingPanel;
    public GameObject returnStartScene;
    public bool isActive = false;

    public void ShowSetting()
    {
        
        settingPanel.GetComponent<Panel_Setting>().Show();
        isActive = true;
    }

    public void HideSetting()
    {
        bkPanel.SetActive(false);
        settingPanel.SetActive(false);
        isActive = false;
    }

    /// <summary>
    /// 切换场景面板组件是否显示
    /// </summary>
    /// <param name="s"></param>
    /// <param name="l"></param>
    private void IsShow(Scene s, LoadSceneMode l)
    {
	    if (s.buildIndex == 0)
        {
            returnStartScene.SetActive(false);
        }
        else
        {
            returnStartScene.SetActive(true);
        }
    }

}