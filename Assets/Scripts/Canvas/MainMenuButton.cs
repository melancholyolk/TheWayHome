using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

/// <summary>
/// 开始界面动画控制
/// </summary>
public class MainMenuButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public Text text;
    public Animator textAnimator;
   

    public enum ButtonType
    { 
        Start,
        Exit,
        Setting
    }
    public ButtonType btn_type = ButtonType.Start;


    private bool m_IsShow = false;
    
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_IsShow = true;
        StartCoroutine(FadaIn());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsShow = false;
        StartCoroutine(FadaOut());
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (btn_type)
        {
            case ButtonType.Start:
                {
                    SceneManager.LoadScene("ConnectScene");
                    break;
                }
            case ButtonType.Exit:
                {
#if UNITY_EDITOR
	                UnityEditor.EditorApplication.isPlaying = false;
#endif
                    Application.Quit();
                    break;
                }
            case ButtonType.Setting:
                {
                    break;
                }
        }

    }

    IEnumerator FadaIn()
    {
        textAnimator.Play("StartGame",0, 0);
        float rate = 0;
        float time = 0;
        while (rate < 1f)
        {
            if (!m_IsShow)
            {
                break;
            }
            
            time += Time.deltaTime;
            rate = Mathf.Lerp(0, 1, time);
            text.color = new Color(1,1,1,rate);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    IEnumerator FadaOut()
    {
	    textAnimator.Play("EndGame", 0, 0);
        float rate = 1;
        float time = 0;
        Vector2 oriPos = text.transform.position;
        while (rate > 0f)
        {
            if (m_IsShow)
            {
                break;
            }
            time += Time.deltaTime*3;
            rate = Mathf.Lerp(1, 0, time);
            text.transform.position = Vector2.Lerp(text.transform.position, oriPos, time);
            text.color = new Color(1, 1, 1, rate);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
