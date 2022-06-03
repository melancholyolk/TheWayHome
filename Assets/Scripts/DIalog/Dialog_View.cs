using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
///对话视图
/// 点击panel下一句话
/// ori显示玩家/首先发起对话
/// tar非玩家/被对话
/// </summary>
public class Dialog_View : MonoBehaviour
{
    public bool playOnAwake;
    public bool isHightLightImg;
    [Header("Text")] public string content;
    [FormerlySerializedAs("tDuration")] public float textDuration = 0.2f; //控制效果速度
    public MyType type;
    [Header("Sprites")] public Sprite ori;
    public Sprite tar;
    [FormerlySerializedAs("duration")] public float spriteDuration; //图片渐变时间

    public bool isPlaying;
    public float waitTime;

    public enum MyType
    {
        FADE,
        TAP,
        NONE
    }

    private Image i_ori;
    private Image i_tar;
    private Text t_text;
    private List<MyDialogForm> _listDialog = new List<MyDialogForm>();
    private List<MyDialogForm> d_temp;
    private Animator ani;
    private Coroutine _internalTimer;
    private Color i_tarOld, i_oriOld;
    [SerializeField] private Color Color_text;

    [Space(25)] [SerializeField] private int d_index = -1;


    private void Awake()
    {
        //+++++++++++++++++++++++++++Debug++++++++++++++++++++++++++++++++
        // List_Dialog = new List<MyDialogForm>();
        // List_Dialog.Add(new MyDialogForm(0, "你好！"));
        // List_Dialog.Add(new MyDialogForm(1, "你叫什么！"));
        // List_Dialog.Add(new MyDialogForm(0, "我叫Json！"));
        // List_Dialog.Add(new MyDialogForm(1, "我叫Mary！"));

        i_ori = transform.GetChild(1).GetComponent<Image>();
        i_tar = transform.GetChild(2).GetComponent<Image>();
        t_text = transform.GetChild(0).GetComponent<Text>();


        ani = GetComponent<Animator>();
        Color_text = t_text.color;
        i_ori.sprite = ori;
        i_tar.sprite = tar;

        i_oriOld = i_tarOld = Color.clear;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (d_index < d_temp.Count)
                NextSentence();
        }
    }

    /// <summary>
    /// 显示视图
    /// </summary>
    public void ShowView()
    {
        gameObject.SetActive(true);
        d_index = -1;
        t_text.text = "";
        isPlaying = false;
        //正放
        ani.Play("Show", 0, 0);
        if (playOnAwake) NextSentence();
        StartCoroutine(ShowOri());
        StartCoroutine(ShowTar());
    }

    /// <summary>
    /// 隐藏视图
    /// </summary>
    /// <returns></returns>
    public IEnumerator HideView()
    {
        //倒放
        ani.StartPlayback();
        ani.speed *= -1;
        ani.Play("Show", 0, 1);
        yield return new WaitUntil(() => ani.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0);
        //还原
        ani.StartPlayback();
        ani.speed *= -1;
        d_index = -1;

        i_oriOld = i_tarOld = Color.clear;
        i_ori.GetComponent<Fade_Image>().Stop();
        i_tar.GetComponent<Fade_Image>().Stop();
        t_text.GetComponent<Fade_Text>().Stop();
        StopAllCoroutines();

        t_text.text = "";
        _listDialog.Clear();
        d_temp.Clear();
        gameObject.SetActive(false);
    }

    private IEnumerator ShowOri()
    {
        isPlaying = true;
        Fade_Image fade = i_ori.GetComponent<Fade_Image>();
        yield return new WaitUntil(fade.FadeIsOver);
        i_ori.sprite = ori;
        fade.FadeIn(spriteDuration);
        fade.Begin();

        yield return new WaitUntil(fade.FadeIsOver);

        isPlaying = false;
    }

    private void HideOri()
    {
        Fade_Image fade = i_ori.GetComponent<Fade_Image>();
        fade.FadeOut(spriteDuration);
        fade.Begin();
    }

    private IEnumerator ShowTar()
    {
        isPlaying = true;
        Fade_Image fade = i_ori.GetComponent<Fade_Image>();
        yield return new WaitUntil(fade.FadeIsOver);
        i_tar.sprite = tar;
        fade.FadeIn(spriteDuration);
        fade.Begin();
        yield return new WaitUntil(fade.FadeIsOver);
        isPlaying = false;
    }

    private void HideTar()
    {
        Fade_Image fade = i_tar.GetComponent<Fade_Image>();
        fade.FadeOut(spriteDuration);
        fade.Begin();
    }

    private void ShowSentence()
    {
        switch (type)
        {
            case MyType.FADE:
                StartCoroutine(ShowSentence_Fade(textDuration));
                break;
            case MyType.TAP:
                StartCoroutine(ShowSentence_Tap(textDuration));
                break;
            case MyType.NONE:
                StartCoroutine(ShowSentence_Tap(0));
                break;
        }
    }

    private void HideSentence()
    {
        switch (type)
        {
            case MyType.FADE:
                StartCoroutine(HideSentence_Fade(textDuration));
                break;
            case MyType.TAP:
                StartCoroutine(HideSentence_Fade(textDuration));
                break;
            case MyType.NONE:
                StartCoroutine(HideSentence_Fade(0));
                break;
        }
    }

    /// <summary>
    /// 播放下一句话
    /// </summary>
    public void NextSentence()
    {
        if (_internalTimer != null)
            StopCoroutine(_internalTimer);
        _internalTimer = StartCoroutine(InternalTimer(waitTime));
        if (d_temp == null || d_temp.Count <= 0)
        {
            Debug.LogError("对话列表为空！");
            return;
        }

        if (isPlaying)
        {
            i_ori.GetComponent<Fade_Image>().Stop();
            i_tar.GetComponent<Fade_Image>().Stop();
            t_text.GetComponent<Fade_Text>().Stop();
            StopAllCoroutines();
            t_text.text = d_temp[d_index].content;
            t_text.color = Color_text;
            isPlaying = false;
            return;
        }

        if (++d_index < d_temp.Count)
        {
            content = d_temp[d_index].content;
            HideSentence();
            ShowSentence();
            if (isHightLightImg)
            {
                StartCoroutine(HightLightUntilFade(d_temp[d_index].index, d_index));
            }
        }
        else
        {
            HideSentence();
            HideOri();
            HideTar();
            StartCoroutine(HideView());
        }
    }

    /// <summary>
    /// 播放指定对话
    /// </summary>
    public void NextTarSentence(int index)
    {
        print("nexttar " + index);
        if (d_temp == null || d_temp.Count <= 0)
        {
            Debug.LogError("对话列表为空！");
            return;
        }

        if (index < d_temp.Count)
        {
            content = d_temp[index].content;
            HideSentence();
            ShowSentence();
            if (isHightLightImg)
            {
                StartCoroutine(HightLightUntilFade(d_temp[index].index, index));
            }
        }
        else
        {
            HideSentence();
            HideOri();
            HideTar();
            StartCoroutine(HideView());
        }
    }

    /// <summary>
    /// 改变ori图片
    /// </summary>
    public void ChangeOri()
    {
        HideOri();
        StartCoroutine(ShowOri());
    }

    /// <summary>
    /// 改变tar图片
    /// </summary>
    public void ChangeTar()
    {
        HideTar();
        StartCoroutine(ShowTar());
    }

    /// <summary>
    /// 初始化对话列表
    /// </summary>
    /// <param name="dialog"></param>
    public void SetDialogList(List<MyDialogForm> dialog)
    {
        if (dialog != null)
        {
            if (_listDialog.Count > 0)
            {
                _listDialog.AddRange(dialog);
                d_temp.AddRange(dialog);
                return;
            }

            _listDialog = new List<MyDialogForm>(dialog);
            d_temp = new List<MyDialogForm>(_listDialog);
        }
        else
        {
            throw new Exception("对话列表为空");
        }
    }

    /// <summary>
    /// 淡入显示文字
    /// </summary>
    private IEnumerator ShowSentence_Fade(float duration)
    {
        isPlaying = true;
        Fade_Text fade = t_text.GetComponent<Fade_Text>();
        yield return new WaitUntil(fade.FadeIsOver);
        fade.FadeIn(duration);
        fade.Begin();
        yield return new WaitForSeconds(0.1f * textDuration);
        t_text.text = content;
        yield return new WaitUntil(fade.FadeIsOver);
        isPlaying = false;
    }

    /// <summary>
    /// 打字机显示文字
    /// </summary>
    private IEnumerator ShowSentence_Tap(float duration)
    {
        yield return new WaitUntil(t_text.GetComponent<Fade_Text>().FadeIsOver);
        t_text.color = Color_text;
        t_text.text = "";
        if (duration <= 0)
        {
            print(duration);
            t_text.text = content;
            isPlaying = false;
            yield break;
        }

        int p = 0; //字符指针
        while (p <= content.Length)
        {
            isPlaying = true;
            yield return new WaitForSeconds(duration);
            string t = content.Substring(0, p);
            t_text.text = t;
            p++;
        }

        isPlaying = false;
    }

    private IEnumerator HideSentence_Fade(float duration)
    {
        isPlaying = true;
        Fade_Text fade = t_text.GetComponent<Fade_Text>();
        yield return new WaitUntil(fade.FadeIsOver);
        fade.FadeOut(duration);
        fade.Begin();
        yield return new WaitForSeconds(0.1f * textDuration);
        if (d_index == d_temp.Count) t_text.text = "";
        yield return new WaitUntil(fade.FadeIsOver);
        isPlaying = false;
    }

    /// <summary>
    /// 高亮正在对话的角色
    /// </summary>
    /// <param name="img">操作的对象</param>
    /// <returns>图片的原来颜色</returns>
    private void HightLightImage(Image img, ref Color color)
    {
        if (color == Color.clear)
        {
            color = img.color;
        }

        img.color = Color.white;
    }

    private IEnumerator HightLightUntilFade(int imgIndex, int sentenceIndex)
    {
        if (sentenceIndex <= 1) yield return new WaitForSeconds(spriteDuration);
        switch (imgIndex)
        {
            case 0:
                print(i_ori.GetComponent<Fade_Image>().FadeIsOver());
                yield return new WaitUntil(i_ori.GetComponent<Fade_Image>().FadeIsOver);
                HightLightImage(i_ori, ref i_oriOld);
                if (i_tarOld != Color.clear)
                    i_tar.color = i_tarOld;
                break;
            default:
                yield return new WaitUntil(i_tar.GetComponent<Fade_Image>().FadeIsOver);
                HightLightImage(i_tar, ref i_tarOld);
                if (i_oriOld != Color.clear)
                    i_ori.color = i_oriOld;
                break;
        }
    }

    IEnumerator InternalTimer(float tar)
    {
        float timer = 0;
        while (timer < tar)
        {
            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        NextSentence();
    }
}

/// <summary>
/// 自定义对话数据结构
/// </summary>
public struct MyDialogForm
{
    public int index;
    public string content;

    public MyDialogForm(int index, string content)
    {
        this.index = index;
        this.content = content;
    }
}