using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingAnimation : MonoBehaviour
{
    string[] texts;
    Image _img;
    // Start is called before the first frame update
    private void Start()
    {
        _img = GetComponentInChildren<Image>();
    }
    public void LoadRandom()
    {
        int index = Random.Range(0,texts.Length-1);
        
    }
}
