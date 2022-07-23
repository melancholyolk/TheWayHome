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
		public bool isUsing;
		[ReadOnly]
		public bool disable;
		[FormerlySerializedAs("Id")] 
		[ReadOnly]
		public string id = Guid.NewGuid().ToString().Replace("-", "").ToLower();

		public virtual void Awake()
		{
			id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
		}
		[ContextMenu("Reload")]
		public void ReloadAll(){
			Item[] items = GameObject.FindObjectsOfType<Item>();
			for(int i = 0; i < items.Length; i++)
			{
				items[i].Reload();
			}
		}
		public void Reload()
		{
			id = Guid.NewGuid().ToString().Replace("-", "").ToLower();
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
