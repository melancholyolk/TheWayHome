using System;
using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 动作
	/// </summary>
	
	public class Actions
	{
		public string itemId;
		public string targetId;
		public bool needSync;
		[ReadOnly]
		public string Id;
		protected Config m_Config;
		public virtual void Init(string id,Config config)
		{
			Id = id;
			m_Config = config;
		}
		public void SyncAction()
		{
			if (needSync)
			{
				MonoECSInteract.Instance.CmdAction(itemId, Id, targetId);
			}
			else
			{ 
				DoAction(); 
			}
				
		}
		public virtual void DoAction()
		{
			
		}
	}
}

