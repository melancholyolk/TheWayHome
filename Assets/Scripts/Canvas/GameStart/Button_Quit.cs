using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Quit : MonoBehaviour
{
    private GameObject panel;
    private void Start()
    {
        panel = transform.GetChild(0).gameObject;
    }

    public void OnClick()
    {
        Color a = panel.GetComponent<Image>().color;
        a.a = 0.8f;
        panel.GetComponent<Image>().color = a;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        print("quit");
#else
        Application.Quit();
        print("quitgame");
#endif
    }
}
