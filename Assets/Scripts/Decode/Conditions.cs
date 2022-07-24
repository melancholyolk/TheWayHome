using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Decode
{
	public abstract class Conditions
	{
		public string name;
		protected Config m_Config;
		
		public virtual bool Accept()
		{
			return false;
		}

		public virtual void Start(Config config)
		{
			m_Config = config;
		}
		public virtual void Update()
		{
			
		}

		
		public virtual ConditionInput.InputResult CheckInput(KeyCode key)
		{
			return ConditionInput.InputResult.False;
		}
		public virtual bool DoInput(KeyCode key)
		{
			return false;
		}
	}
}

