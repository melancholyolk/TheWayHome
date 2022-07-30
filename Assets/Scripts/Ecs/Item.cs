using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Decode
{
	public class Item : SerializedMonoBehaviour
	{
		[ReadOnly]
		[NonSerialized]
		public bool isUsing = false;
		[ReadOnly]
		[NonSerialized]
		public bool disable = false;
		[FormerlySerializedAs("Id")] 
		[ReadOnly]
		[SerializeField]
		public string id;
		public void Reload()
		{
			id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
		}

		private void Start()
		{
			MonoECSInteract.Instance.AddGScript(id, this);
			isUsing = false;
			disable = false;
		}

		public virtual void DoAction(string actionId , string targetId)
		{
			
		}
	}
}
