using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSetting", order = 4)]
public class GameSetting : SerializedScriptableObject
{
	public float volume;
	public int distinguish;
	public bool isFullScreen;
	
}
