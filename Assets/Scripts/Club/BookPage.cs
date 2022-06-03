using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookPage : MonoBehaviour
{
    public PropInfo info;
    public Image back;
    public Text name;
    public Text describe;
    public bool is_page;
    public void SetPageInfo(PropInfo ifo)
    {
        this.info = ifo;
        if (is_page)
        {
            if (info.prop_name != "")
            {
                back.color = new Color(1, 1, 1, 1);
                back.sprite = info.prop_sprite;
                name.text = info.prop_name;
                describe.text = info.prop_describe;
            }
            else
            {
                back.color = new Color(1, 1, 1, 0);
                name.text = "";
                describe.text = "";
            }
        }
        else
        {
            if (info.prop_name != "")
            {
                back.sprite = info.prop_sprite;
                name.text = info.prop_name;
                describe.text = info.prop_describe;
            }
        }
    }
}
