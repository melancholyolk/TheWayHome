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
		setting.ShowSetting();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && SceneTransition.IsOperatable)
		{
			if (SceneManager.GetActiveScene().buildIndex != 0)
			{
				setting.settingPanel.GetComponent<Panel_Setting>().Hide();
			}
		}
	}
}