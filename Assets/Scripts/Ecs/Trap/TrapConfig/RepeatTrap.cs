using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Decode
{
	public class RepeatTrap : TrapConfig
	{
		[Tooltip("重复次数,负数表示无限次")]
		public int repeatNum = -1;
		[Tooltip("间隔时间")]
		public float repeatInterval = 1;
		private int m_CurNum = 0;
		private float m_Cd = 0;
		private bool is_active = false;

		public override void DoActions()
		{
			is_active = true;
		}

		public override void Update()
		{
			if(is_active)
			{
				if (repeatNum < 0 || m_CurNum < repeatNum)
				{
					m_Cd += Time.deltaTime;
					if (m_Cd >= repeatInterval)
					{

						m_Cd = 0;
						m_CurNum++;
						RepeatAction();
					}
				}
				else{
					m_Cd = 0;
					m_CurNum = 0;
					is_active = false;
				}
			}
			
		}

		private void RepeatAction()
		{
			for (int i = 0; i < actions.Length; i++)
				actions[i].SyncAction();
		}
	}
}
