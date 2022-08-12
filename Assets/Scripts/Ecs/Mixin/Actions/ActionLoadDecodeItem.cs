using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Decode
{
	public class ActionLoadDecodeItem : Actions
	{
		public string address;
		public Camera decodeCamera;
		public VolumeProfile postProcessProfile;
		public override void DoAction()
		{
			
			var item = MonoECSInteract.Instance.GetItem(itemId) as ObtainItems;
			// var go = GameObject.Instantiate(decodePrefab,decodeCamera.transform.TransformPoint(Vector3.forward * 10), Quaternion.identity);
			var handle = Addressables.InstantiateAsync(address,decodeCamera.transform.TransformPoint(Vector3.forward * 10), Quaternion.identity);
			handle.Completed += (l) =>
			{
				if (l.Status == AsyncOperationStatus.Succeeded)
				{
					// go.transform.parent = item.transform;
					var go = l.Result;
					go.transform.LookAt(decodeCamera.transform);
					go.SetActive(true);
					var input = go.GetComponent<DecodeBaseInput>();
					item.decodes.Add(input);
					input.Initialize(item,m_Config);
					
				}
			};
			OperationControl.Instance.is_decoding = true;
			postProcessProfile.components[0].active = true;
		}
	}
}

