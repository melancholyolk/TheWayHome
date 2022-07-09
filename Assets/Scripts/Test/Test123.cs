using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Test123 : MonoBehaviour
{
	[EnumToggleButtons]
	public InfoMessageType SomeEnum;
 
	public bool IsToggled;
 
	[ShowIf("SomeEnum", InfoMessageType.Info)]
	public Vector3 Info;
 
	[ShowIf("SomeEnum", InfoMessageType.Warning)]
	public Vector2 Warning;
 
	[ShowIf("SomeEnum", InfoMessageType.Error)]
	public Vector3 Error;

	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
