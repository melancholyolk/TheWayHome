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
        //bkPanel.SetActive(true);
        //settingPanel.SetActive(true);
        settingPanel.GetComponent<Panel_Setting>().Show();
        isActive = true;
        // GameObject.Find("StopToggle").SendMessage("Pause");
    }

    public void HideSetting()
    {
        bkPanel.SetActive(false);
        settingPanel.SetActive(false);
        isActive = false;
        // GameObject.Find("StopToggle").SendMessage("Run");
    }

    /// <summary>
    /// 切换场景面板组件是否显示
    /// </summary>
    /// <param name="s"></param>
    /// <param name="l"></param>
    private void IsShow(Scene s, LoadSceneMode l)
    {
        print("isShow" + s.buildIndex);
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