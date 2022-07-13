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
		protected override void DoAction()
		{
			var go = GameObject.Instantiate(decodePrefab, decodeCamera.transform.forward.normalized,
				Quaternion.identity, item.transform);
			go.transform.LookAt(decodeCamera.transform);
			var input = go.GetComponent<BaseInput>();
			input.Initialize(item);
			isDone = true;
			MonoECSInteract.Instance.CmdIsUsing(item.Id,true);
		}
	}
}

