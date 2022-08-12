using System.Collections;
using System.Collections.Generic;
using Decode;
using Decode.Buff;
using UnityEngine;

public class LocalPlayerAtrributeDeal
{
	public static LocalPlayerAtrributeDeal Instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = new LocalPlayerAtrributeDeal();
			}

			return m_Instance;
		}
	}
	private static LocalPlayerAtrributeDeal m_Instance;
	private List<Buff> m_Buffs = new List<Buff>();
	
	public void Execute()
	{
		foreach (var buff in m_Buffs)
		{
			buff.Execute();
		}
	}

	public void AddListener(Buff buff)
	{
		m_Buffs.Add(buff);
		buff.OnAdd();
	}

	public void RemoveListener(Buff buff)
	{
		m_Buffs.Remove(buff);
		buff.OnRemove();
	}
}
