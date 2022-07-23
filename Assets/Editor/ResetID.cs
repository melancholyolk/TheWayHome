using Decode;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Item))]
public class ResetID : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		Item myScript = (Item) target;
		if (GUILayout.Button("重新生成ID"))
		{
			
		}
	}
}