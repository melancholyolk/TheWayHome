using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Decode
{
    public class CoolingTrap : TrapConfig
    {
        public float coolingTime;
        private float m_CD = 0;
		private bool m_isCooling = false;

		public override void DoActions()
		{
			if (!m_isCooling)
			{
				m_isCooling = true;
				for (int i = 0; i < actions.Length; i++)
					actions[i].SyncAction();
			}
		}
		public override void Update()
		{
			if(m_isCooling)
			{
				m_CD += Time.deltaTime;
				if(m_CD >= coolingTime)
				{
					m_CD = 0;
					m_isCooling = false;
				}
			}
		}
	}
}

