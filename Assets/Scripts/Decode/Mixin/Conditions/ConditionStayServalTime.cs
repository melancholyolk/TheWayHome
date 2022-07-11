using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	public class ConditionStayServalTime : Conditions
	{
		public float time;

		private float m_Timer;

		public override bool Accept()
		{
			m_Timer += Time.deltaTime;
			return m_Timer > time;
		}
	}
}

