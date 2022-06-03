using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetInfo : MonoBehaviour
{
    private PropInfo prop_info;
    public Text name;
    private Text describe;
    public void SetProp(PropInfo prop,Text des)
    {
        prop_info = prop;
        name.text = prop.prop_name;
        describe = des;
    }

    public void IsChose()
    {
        describe.text = prop_info.prop_describe;
    }
}
