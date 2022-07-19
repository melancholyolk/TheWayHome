using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Decode
{
	/// <summary>
	/// 获得物品类
	/// 挂在场景中的物体上
	/// </summary>
	public class ObtainItems : Item
	{
		public bool canUse = false;

		[ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "name")]
		[Searchable]
		public List<ObtainConfig> configs;

		[HideInInspector]
		public GameObject pre;

		private void Awake()
		{
			for(int i = 0; i < configs.Count; i++)
			{
				configs[i].Awake(i.ToString());
				configs[i].Init(this);
			}
		}

		#region MonoAPI

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				MonoECSInteract.Instance.AddScript(this);
				foreach (var config in configs)
				{
					foreach (var condition in config.conditions)
					{
						condition.Start();
					}
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				MonoECSInteract.Instance.RemoveScript(this);
			}
		}

		#endregion

		#region PrivateFunction
		
		#endregion
		
		#region publicAPI

		public void CheckKeyInput(KeyCode key)
		{
			for (var i = configs.Count - 1; i >= 0; i--)
			{
				configs[i].CheckKeyInput(key);
			}
		}

		public void CheckAllConfigs()
		{
			for (var i = configs.Count - 1; i >= 0; i--)
			{
				configs[i].DoConditions();
			}
		}

		public override void DoAction(string actionId, string targetId)
		{
			foreach (var config in configs)
			{
				foreach (var action in config.actions)
				{
					if (action.Id == actionId)
					{
						action.DoAction();
					}
				}
			}
		}

		#endregion
	}
}