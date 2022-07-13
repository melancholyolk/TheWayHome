using System.Collections;
using System.Collections.Generic;
using Decode;
using Unity.Mathematics;
using UnityEngine;
/// <summary>
/// 加载角色
/// </summary>
public class GameLoader : MonoBehaviour
{
	public enum Mode
	{
		local,
		network,
	}

	public Mode mode;

	public List<GameObject> loaded;
	// Start is called before the first frame update
    void Start()
    {
	    switch (mode)
	    {
		    case Mode.local:
			    foreach (var l in loaded)
			    {
				    l.SetActive(true);
			    }
			    break;
		    case Mode.network:
			    break;
	    }
    }
    
}
