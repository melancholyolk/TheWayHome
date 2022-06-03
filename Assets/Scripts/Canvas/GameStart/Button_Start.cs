using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


/// <summary>
/// 开始游戏
/// </summary>
public class Button_Start : MonoBehaviour
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
      GameObject.FindWithTag("CanvasManager").GetComponent<SceneTransition>().LoadNextScene();
   }
}
