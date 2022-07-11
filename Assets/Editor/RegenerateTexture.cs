using System.Collections;
using System.Collections.Generic;
using GameUtil;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Cube6SideTexture))]
public class RegenerateTexture : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Cube6SideTexture myScript = (Cube6SideTexture) target;
		if (GUILayout.Button("重新生成图片"))
		{
			myScript.GenMergedTexture();
			myScript.Start();
		}
		if (GUILayout.Button("保存图片"))
		{
			myScript.SaveMergedTexture();
		}
	}
}
