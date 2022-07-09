using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Sirenix.Serialization;
namespace Decode
{
	public abstract class DecodeWay : MonoBehaviour
	{
		
		public virtual void IsComplete(){}
		public virtual void AddListener(){}
		public virtual void OnComplete(){}
		
	}
}

