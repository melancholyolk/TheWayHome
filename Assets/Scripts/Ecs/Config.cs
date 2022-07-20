using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Decode
{
	public class Config
	{
		public Actions[] actions;
		[PropertyOrder(-3)]
		[ShowInInspector]
		[ReadOnly]
		private string m_Id;
		[PropertyOrder(-2)]
		[ReadOnly] 
		public bool isComplete;
		public void Awake(string id)
		{
			m_Id = id;
			for (int i = 0; i < actions.Length; i++)
			{
				actions[i].Init(m_Id+i.ToString(),this);
			}
		}
		public void Init(Item item)
		{
			for (int i = 0; i < actions.Length; i++)
			{
				actions[i].itemId = item.id;
			}
		}

		public void DoActions(string actionId,string targetId)
		{
			for(int i = 0;  i < actions.Length;i++)
			{
				Actions action = actions[i];
			    if(action.Id.Equals(actionId) && action.needSync)
				{
					action.DoAction();
				}
			}
		}

	}
}