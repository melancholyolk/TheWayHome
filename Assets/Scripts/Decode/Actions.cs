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
	
	public class Actions : ICloneable
	{
		public string itemId;
		public string targetId;
		public bool needSync;
		[ReadOnly]
		public string Id;
		protected Config m_Config;
		public virtual void Init(string itemid,string id,Config config)
		{
			itemId = itemid;
			Id = id;
			m_Config = config;
		}
		public void SyncAction()
		{
			if (needSync)
			{
				// Debug.Log(itemId + " 1 " + Id + " 2 " + targetId);
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

		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}

