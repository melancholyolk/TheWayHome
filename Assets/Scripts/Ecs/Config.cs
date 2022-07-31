using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Decode
{
	public class Config : ICloneable
	{
		public Actions[] actions;
		[PropertyOrder(-3)]
		[ShowInInspector]
		[ReadOnly]
		public string Id;
		[PropertyOrder(-2)]
		[ReadOnly] 
		public bool isComplete;

		[ReadOnly] 
		public bool isUsing;
		public void Awake(string itemid,string id)
		{
			Id = id;
			for (int i = 0; i < actions.Length; i++)
			{
				actions[i].Init(itemid, Id+i.ToString(),this);
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

		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}