using System.Collections;
using System.Collections.Generic;
using Decode;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObtainItemResources", order = 2)]
public class ObtainItemResources : SerializedScriptableObject
{
	[ShowInInspector]
	public Dictionary<string,ObtainItems> itemSet;
}
