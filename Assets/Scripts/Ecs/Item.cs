using System;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Decode
{
	public class Item : SerializedMonoBehaviour
	{
		[ReadOnly]
		public bool isUsing;
		[ReadOnly]
		public bool disable;
		[ReadOnly]
		public string Id;

		private void Reset()
		{
			Guid guid = Guid.NewGuid();
			Id = guid.ToString().Replace("-", "").ToLower();
		}

		private void Start()
		{
			MonoECSInteract.Instance.AddGScript(Id, this);
		}

		public virtual void DoAction(string actionId , string targetId)
		{
			
		}
	}
}
