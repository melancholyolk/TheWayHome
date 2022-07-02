using System.Collections;
using System.Collections.Generic;
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
	public GameObject spawner;
	// Start is called before the first frame update
    void Start()
    {
	    switch (mode)
	    {
		    case Mode.local:
			    var go = Instantiate(spawner);
			    go.transform.position = Vector3.zero;
			    break;
		    case Mode.network:
			    break;
	    }
    }
    
}
