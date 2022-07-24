using Decode;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Item))]
public class ResetID : Editor
{
	[MenuItem("WayHome/ReloadItemID")]
	public static void RealodAll()
	{
		Item[] items = GameObject.FindObjectsOfType<Item>();
		for(int i = 0; i < items.Length; i++)
		{
			items[i].Reload();
		}
	}
}