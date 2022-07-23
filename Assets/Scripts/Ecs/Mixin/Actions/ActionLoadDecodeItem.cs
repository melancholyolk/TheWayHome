using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace Decode
{
	public class ActionLoadDecodeItem : Actions
	{
		public GameObject decodePrefab;
		public Camera decodeCamera;
		public VolumeProfile postProcessProfile;
		public override void DoAction()
		{
			var item = MonoECSInteract.Instance.GetItem(itemId) as ObtainItems;
			var go = GameObject.Instantiate(decodePrefab,decodeCamera.transform.TransformPoint(Vector3.forward), Quaternion.identity);
			go.transform.parent = item.transform;
			go.transform.LookAt(decodeCamera.transform);
			var input = go.GetComponent<DecodeBaseInput>();
			item.decodes.Add(input);
			input.Initialize(item,m_Config);

			postProcessProfile.components[0].active = true;
		}
	}
}

