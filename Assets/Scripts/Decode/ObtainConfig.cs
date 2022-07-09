using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Decode
{
	public class ObtainConfig
	{
		public string name;
		public Conditions[] conditions;
		public Actions[] actions;
		
		public virtual bool AllAccept()
		{
			foreach (var condition in conditions)
			{
				
			}
			return true;
		}

		public virtual bool OneAccept()
		{
			return false;
		}
		
		protected virtual void Complete()
		{
			// isComplete = true;
			// m_CanvasManager.is_decoding = false;
			// if (propId.Count > 0)
			// {
			// 	for (int i = 0; i < propId.Count; i++)
			// 	{
			// 		m_CanvasManager.PickUpStart(propId[i], 1);
			// 	}
			// }
			//
			// if (propId.Count == 0 && GetComponent<DecodeCallBack>())
			// {
			// 	Complete(GetComponent<DecodeCallBack>());
			// }
		}

		protected virtual void Complete(DecodeCallBack callBack)
		{
			callBack.CallBack();
		}

		public void DoConditions()
		{
			foreach (var condition in conditions)
			{
				if(!condition.Accept())
					return;
			}

			foreach (var action in actions)
			{
				action.DoAction();
			}
		}
	}
}

