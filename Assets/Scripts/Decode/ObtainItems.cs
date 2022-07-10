using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.ProGrids;
using UnityEngine.Serialization;
namespace Decode
{
	/// <summary>
	/// 获得物品类
	/// 挂在场景中的物体上
	/// </summary>
	public class ObtainItems : SerializedMonoBehaviour
	{
		public bool canUse = false;

		public GameObject pre;

		
		
		
		public List<int> propId;

		[ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "name")]

		[Searchable]
		public ObtainConfig[] configs;

		

		private GameObject m_Ori;
		
		#region MonoAPI
		

		private void Start()
		{

		}

		private void Update()
		{
			
			
			// if (canUse && !isUsing && !isComplete && m_CanvasManager.CanOperate() && Input.GetKeyDown(KeyCode.F))
			// {
			// 	if (m_Ori)
			// 		Destroy(m_Ori);
			// 	if (!GetComponent<DeCode>() && pre == null)
			// 	{
			// 		Complete();
			// 		return;
			// 	}
			//
			// 	isUsing = true;
			// 	m_CanvasManager.is_decoding = true;
			// 	m_Ori = Instantiate(pre, transform);
			// 	m_Ori.transform.position =
			// 		GameObject.FindWithTag("DecodeCamera").transform.position + Vector3.forward * 60 +
			// 		pre.transform.position;
			// 	m_Ori.transform.LookAt(GameObject.FindWithTag("DecodeCamera").transform.position);
			// }
			// else if (isUsing && canUse && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F)))
			// {
			// 	Destroy(m_Ori);
			// 	isUsing = false;
			// 	m_CanvasManager.is_decoding = false;
			// }
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				MonoECSDecode.Instance.AddScript(this);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				MonoECSDecode.Instance.RemoveScript(this);
			}
		}

		#endregion

		#region PrivateFunction

		protected virtual void Complete()
		{
			// isComplete = true;
			// m_CanvasManager.is_decoding = false;
			if (propId.Count > 0)
			{
				for (int i = 0; i < propId.Count; i++)
				{
					// m_CanvasManager.PickUpStart(propId[i], 1);
				}
			}

			// if (propId.Count == 0 && GetComponent<DecodeCallBack>())
			// {
			// 	Complete(GetComponent<DecodeCallBack>());
			// }
		}

		protected virtual void Complete(DecodeCallBack callBack)
		{
			callBack.CallBack();
		}

		private GameObject[] GetItemByID()
		{
			return default;
		}

		#endregion


		#region publicAPI

		public void CheckAllConfigs()
		{
			foreach (var config in configs)
			{
				config.DoConditions();
			}
		}
		

		#endregion
		

		public void PlayerNear()
		{
			canUse = true;
		}

		public void PlayerLeave()
		{
			canUse = false;
			Destroy(m_Ori);
			// isUsing = false;
		}

		
	}
}