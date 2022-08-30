using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using DepthOfField = UnityEngine.Rendering.PostProcessing.DepthOfField;

namespace Decode
{
	/// <summary>
	/// 判断自己是否正确
	/// 并执行action
	/// </summary>
	public class DecodeBaseInput : SerializedMonoBehaviour
	{
		public Actions[] actions;
		public VolumeProfile profile;
		private ObtainItems m_Parent;
		private Config m_Config;
		
		public void Initialize(ObtainItems parent,Config config)
		{
			m_Parent = parent;
			m_Config = config;
		}
		
		public virtual void CheckInput()
		{
			
		}

		public virtual void OnInterrupt()
		{
			m_Config.isComplete = false;
			profile.components[0].active = false;
			Destroy(gameObject);
		}

		public virtual void DoActions()
		{
			for (int i = 0; i < actions.Length; i++)
			{
				actions[i].SyncAction();
			}
			profile.components[0].active = false;
			m_Parent.disable = true;
			m_Parent.isUsing = false;
			OperationControl.Instance.is_decoding = false;
			Destroy(gameObject,1.5f);
		}
		/// <summary>
		/// 解密成功后调用
		/// </summary>
		protected virtual IEnumerator Success()
		{
			yield return null;
		}
	}
}