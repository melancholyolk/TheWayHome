using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyPanel : MonoBehaviour
{
    public bool is_used = false;
    public int tag;
    private int toy_tag = 0;
    private ToyTagJudge tag_judge;
    private void Start()
    {
        if (tag != 0)
        {
            tag_judge = this.transform.parent.GetComponent<ToyTagJudge>();
        }
    }
    public void SetUsed(int t)
    {
        is_used = true;
        toy_tag = t;
        if(toy_tag == tag)
        {
            tag_judge.AddTag();
        }
    }
    public void SetNotUsed()
    {
        is_used = false;
        if(toy_tag == tag)
        {
            tag_judge.DeleteTag();
        }
        toy_tag = 0;
    }

}
