using System;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Decode
{
	public class Item : SerializedMonoBehaviour
	{
		[ReadOnly]
		public bool isUsing;
		[ReadOnly]
		public bool disable;
		[FormerlySerializedAs("Id")] 
		[ReadOnly]
		public string id;

		private void Reset()
		{
			Guid guid = Guid.NewGuid();
			id = guid.ToString().Replace("-", "").ToLower();
		}

		private void Start()
		{
			MonoECSInteract.Instance.AddGScript(id, this);
		}

		public virtual void DoAction(string actionId , string targetId)
		{
			
		}
	}
}
