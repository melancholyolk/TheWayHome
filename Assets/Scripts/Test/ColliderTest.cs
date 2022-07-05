using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class ColliderTest : MonoBehaviour
{
	public float offset;
	public float width;

	private void OnValidate()
	{
		
		var go = new GameObject("Test");
		var collider = go.AddComponent(typeof(BoxCollider)) as BoxCollider;
		var pos = go.transform.localPosition;
		
	}
}
