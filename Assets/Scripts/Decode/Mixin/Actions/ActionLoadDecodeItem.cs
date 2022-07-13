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
			var go = GameObject.Instantiate(decodePrefab, decodeCamera.transform.forward.normalized * 1,
				Quaternion.identity);
			go.transform.LookAt(decodeCamera.transform);
			isDone = true;
		}
	}
}

