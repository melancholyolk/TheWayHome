using System;
using System.Collections.Generic;
using Decode;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scriptable
{
	[CreateAssetMenu(fileName = "GameSave", menuName = "ScriptableObjects/GameSave", order = 0)]
	public class GameInfoSave : SerializedScriptableObject
	{
		[Tooltip("该存档是否已经被使用")]
		public bool isUsing;
		public string saveName = "";
		public PlayerInfo playerInfo1;
		public PlayerInfo playerInfo2;
		public GameState.Chapter process;
		public List<ObtainItemInfo> obtainItemses;
		private void OnValidate()
		{
			Debug.Log("Loaded");
		}

		public void ResetSave()
		{
			saveName = "";
			playerInfo1 = new PlayerInfo();
			playerInfo2 = new PlayerInfo();
			process = GameState.Chapter.None;
		}
	}
}