using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	public struct PrefabInfo
	{
		public GameObject prefab;
		public Vector3 localPosition;
		public Vector3 localRotation;

		
	}
	public class ActionLoadPrefab : Actions
	{
		public PrefabInfo[] prefabs;
		private List<GameObject> m_Instances;
		public override void DoAction()
		{
			m_Instances = new List<GameObject>();
			foreach (var prefab in prefabs)
			{
				var go = GameObject.Instantiate(prefab.prefab, prefab.localPosition,
					Quaternion.Euler(prefab.localRotation));
				m_Instances.Add(go);
			}
		}
	}
}

