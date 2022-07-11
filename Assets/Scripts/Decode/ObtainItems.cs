using Sirenix.OdinInspector;
using UnityEngine;
namespace Decode
{
	/// <summary>
	/// 获得物品类
	/// 挂在场景中的物体上
	/// </summary>
	public class ObtainItems : SerializedMonoBehaviour
	{
		public bool canUse = false;
		[ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "name")]
		[Searchable]
		public ObtainConfig[] configs;

		public GameObject pre;
		
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

		public void CheckAllConfigs()
		{
			foreach (var config in configs)
			{
				config.DoConditions();
			}
		}
		

		#endregion
		

		public void PlayerNear()
		{
			canUse = true;
		}

		public void PlayerLeave()
		{
			canUse = false;
		}

		
	}
}