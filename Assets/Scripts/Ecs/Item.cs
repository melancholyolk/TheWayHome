using System;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Decode
{
	public class Item : SerializedMonoBehaviour
	{
		public bool isUsing;
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
