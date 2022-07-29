using System.Collections;
using System.Collections.Generic;
using Util;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(CubeSixSideTexture))]
public class RegenerateTexture : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		CubeSixSideTexture myScript = (CubeSixSideTexture) target;
		if (GUILayout.Button("获得当前材质贴图"))
		{
			myScript.GetCurrentMaterialTexture();
		}
		if (GUILayout.Button("默认UV点"))
		{
			myScript.SetDefault();
		}
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
