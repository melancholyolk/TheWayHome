using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Decode
{
	public class Config
	{
		public Actions[] actions;
		public void Awake()
		{
			for (int i = 0; i < actions.Length; i++)
			{
				actions[i].Awake();
			}
		}
		public void Init(Item item)
		{
			for (int i = 0; i < actions.Length; i++)
			{
				actions[i].itemId = item.Id;
			}
		}

		public void DoActions(string actionId,string targetId)
		{
			for(int i = 0;  i < actions.Length;i++)
			{
				Actions action = actions[i];
			    if(action.Id.Equals(actionId) && action.needSync)
				{
					action.targetId = targetId;
					action.DoAction();
				}
			}
		}

	}
}