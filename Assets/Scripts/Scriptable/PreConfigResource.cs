using System.Collections;
using System.Collections.Generic;
using Decode;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PreConfigResource", order = 3)]
public class PreConfigResource : SerializedScriptableObject
{
	public Config pickConfig;
}
