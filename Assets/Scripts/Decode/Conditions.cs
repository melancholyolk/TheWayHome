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
		
		public virtual bool Accept()
		{
			return false;
		}
	}
}

