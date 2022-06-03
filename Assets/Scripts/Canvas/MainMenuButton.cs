using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 开始界面动画控制
/// </summary>
public class MainMenuButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public Text text;
    private bool is_show = false;
    public Vector2 ori_pos;
    public Vector2 end_pos;

    public enum ButtonType
    { 
        Start,
        Exit,
        Setting
    }
    public ButtonType btn_type = ButtonType.Start;

    void Start()
    {
        ori_pos = this.transform.position;
        end_pos = ori_pos + new Vector2(0, 70);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        is_show = true;
        StartCoroutine(FadaIn());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        is_show = false;
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
        text.GetComponent<Animator>().Play("StartGame",0, 0);
        float rate = 0;
        float time = 0;
        while (rate < 1f)
        {
            if (!is_show)
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
        text.GetComponent<Animator>().Play("EndGame", 0, 0);
        float rate = 1;
        float time = 0;
        while (rate > 0f)
        {
            if (is_show)
            {
                break;
            }
            time += Time.deltaTime*3;
            rate = Mathf.Lerp(1, 0, time);
            text.transform.position = Vector2.Lerp(text.transform.position, ori_pos, time);
            text.color = new Color(1, 1, 1, rate);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
