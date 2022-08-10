using System;
using System.Collections;
using System.Collections.Generic;
using Scriptable;
using UnityEngine;

public class ButtonCreateNewSave : MonoBehaviour
{
	public GameInfoSave[] saves;

	public void ChoseEmptySave()
	{
		for (int i = 0; i < saves.Length; i++)
		{
			if (!saves[i].isUsing)
			{
				saves[i].saveName = "存档" + DateTime.Today;
				GameState.currentSave = saves[i];
				return;
			}
		}
	}
}
