using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingControl : MonoBehaviour
{
    public Setting setting;
    private float timer = 0;
    private void OnEnable()
    {
        if (SceneTransition.IsOperatable)
        {
            timer = 0;
            setting.ShowSetting();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += 0.02f;
        if (Input.GetKeyDown(KeyCode.Escape)&&SceneTransition.IsOperatable&&timer>1.2f)
        {
            timer = 0;
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                // if (!setting.gameObject.activeSelf)
                // {
                //     SceneTransition.IsOperatable = false;
                //     setting.ShowSetting();
                // }
               
                {
                    setting.settingPanel.GetComponent<Panel_Setting>().Hide();
                }
            }
        }
    }
}
