using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerInput : MonoBehaviour
{
    public string code;
    public string endValue;
    public ComputerControl computerControl;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<InputField>().onEndEdit.AddListener(InputEnd);
    }

    private void InputEnd(string value)
    {
        endValue = value;
        if(endValue != code)
        {
            computerControl.Wrong();
        }
        else
        {
            computerControl.Complete(); 
        }
    }
}
