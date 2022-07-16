using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyTagJudge : MonoBehaviour
{
    public int num = 0;
    public void AddTag()
    {
        num++;
        if(num == 4)
        {
            this.transform.parent.GetComponent<ToyManager>().Complete();
        }
    }

    public void DeleteTag()
    {
        num--;
    }
}
