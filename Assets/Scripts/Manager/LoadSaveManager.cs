using System;
using System.Collections;
using System.Collections.Generic;
using Scriptable;
using UnityEngine;
using UnityEngine.UI;

public class LoadSaveManager : MonoBehaviour
{
	public GameInfoSave save;

	private void Awake()
	{
		var text = GetComponentInChildren<Text>();
		var name = save.saveName;
		if (String.IsNullOrEmpty(name))
		{
			text.text = "新的存档";
		}
		else
		{
			text.text = name;
		}
	}

	public void LoadSave()
	{
		if (!save.isUsing)
		{
			save.saveName = "存档" + DateTime.Today;
		}
		GameState.currentSave = save;
		GameState.state = save.process;
		//do something
	}
}
