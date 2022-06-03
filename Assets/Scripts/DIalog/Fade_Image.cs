using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制物体淡入淡出
/// </summary>
public class Fade_Image : MonoBehaviour
{
    private bool f_isover = true;

    private float f_current_a;

    private float f_target_a;

    private float f_time;

    private float f_starttime;

    private bool f_isstart = false;

    private Image f_image;

    private Color f_old;
    // Start is called before the first frame update
    void Awake()
    {
        f_image = GetComponent<Image>();
        f_old = f_image.color;
    }

    public void Begin()
    {
        f_isover = false;
        f_starttime = Time.time;
        f_isstart = true;
    }

    public void Stop()
    {
        f_isover = true;
        f_starttime = 0;
        f_isstart = false;
    }
    // Update is called once per frame
    void Update()
    {
       
        do
        {
            if(f_isstart == false) break;
            if (Time.time - f_starttime > f_time)
            {
                f_isover = true;
                f_isstart = false;
                f_starttime = 0;
                f_image.color = f_old;
            }
            else
            {
                float ratio = (Time.time - f_starttime) / f_time;
                float a = Mathf.Lerp(f_current_a, f_target_a, ratio);
                f_image.color = new Color(f_image.color.r,f_image.color.g,f_image.color.b,a);
            }
        } while (false);
    }
    
    public void FadeIn(float time)
    {
        f_time = time;
        f_target_a = GetComponent<Image>().color.a;
        f_current_a = 0;
    }

    public void FadeOut(float time)
    {
        f_time = time;
        f_target_a = 0;
        f_current_a = f_image.color.a;
    }

    public bool FadeIsOver()
    {
        return f_isover;
    }
}