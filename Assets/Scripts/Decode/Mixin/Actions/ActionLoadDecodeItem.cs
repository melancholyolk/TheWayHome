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
		public ObtainItems parent;
		protected override void DoAction()
		{
			var go = GameObject.Instantiate(decodePrefab, decodeCamera.transform.forward.normalized,
				Quaternion.identity,parent.transform);
			go.transform.LookAt(decodeCamera.transform);
			var input = go.GetComponent<BaseInput>();
			input.Initialize(parent);
			isDone = true;
		}
	}
}

