using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
	public static ObjectPool _instance;
	public GameObject prefab;
	Stack tObj = new Stack();

	const float m_ReleaseInterval = 30;
	float m_Time;
	bool m_needReset = false;

	private void Start()
	{
		_instance = this;
	}
	private void Update()
	{
		m_Time += Time.deltaTime;
		if (m_needReset)
		{
			m_Time = 0;
			m_needReset = false;
		}
		if(m_Time > m_ReleaseInterval && tObj.Count > 0)
		{
			Destroy(tObj.Pop() as GameObject);
			m_Time = 0;
		}

	}

	public GameObject GetGO(int num)
	{
		GameObject go;
		if(tObj.Count > 0)
		{
			go = tObj.Pop() as GameObject;
			go.SetActive(true);
		}
		else
		{
			go = Instantiate(prefab) as GameObject;
		}
		go.GetComponent<PropPick>().SetPropInfo(num);
		return go;
	}

	public void RecycleGo(GameObject go)
	{
		go.SetActive(false);
		tObj.Push(go);
		m_needReset = true;
	}

}
