using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Decode
{
	public class ActionLoadDecodeItem : Actions
	{
		public GameObject decodePrefab;
		public Camera decodeCamera;
		public override void DoAction()
		{
			var item = MonoECSInteract.Instance.GetItem(itemId);
			var go = GameObject.Instantiate(decodePrefab,decodeCamera.transform.TransformPoint(Vector3.forward), Quaternion.identity);
			go.transform.parent = item.transform;
			go.transform.LookAt(decodeCamera.transform);
			var input = go.GetComponent<DecodeBaseInput>();
			input.Initialize(item as ObtainItems);
		}
	}
}

