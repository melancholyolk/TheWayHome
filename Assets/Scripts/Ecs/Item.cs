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
		[ShowInInspector]
		public bool isUsing = false;
		[ReadOnly]
		[NonSerialized]
		[ShowInInspector]
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
			// CanvasManager.Instance.GetComponent<View_Control>().ShowDialog(new List<string>(){id});
			Invoke(nameof(AddListener),1);
		}

		private void AddListener()
		{
			MonoECSInteract.Instance.AddGScript(id, this);
		}
		public virtual void DoAction(string actionId , string targetId)
		{
			
		}
	}
}
