using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Test2 : NetworkBehaviour
{
    private Test1 test;
    // Start is called before the first frame update
    void Start()
    {
        test = GameObject.FindWithTag("PlayerInfo").GetComponent<Test1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(isServer)
            {
                test.SetNum(1);
            }
            else
            {
                test.SetNum(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            CmdChange();
        }
        if (Input.GetKeyDown(KeyCode.E) && test.CanPick())
        {
            test.SetPropPanel(new PropInfo());
        }
    }
    private void CmdChange()
    {
        test.Num();
    }
}
