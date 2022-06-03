using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
///
/// 控制整体游戏进程
/// </summary>
public class SceneTransition : MonoBehaviour
{
    public float wait; //等待时间

    public Text t; //加载关卡显示的文字

    public static bool IsOperatable = true; //可以操作

    public static bool IsPaused = false; //游戏是否暂停

    private GameObject _img;

    private TextTapStyle _tapStyle;

    private string[] _text;

    private void Awake()
    {
        //保证唯一性
        if (GameObject.FindWithTag("Canvas") == null)
        {
            DontDestroyOnLoad(gameObject);
            gameObject.tag = "Canvas";
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _img = transform.GetChild(0).gameObject;
        _img.SetActive(false);
        _tapStyle = transform.GetChild(0).GetChild(1).GetComponent<TextTapStyle>();
        _text = new string[10];

        _text[0] = "人生自古谁无死？留取丹心照汗青";

        _text[1] = "不如学无生，无生即无灭。";

        _text[2] = "死别已吞声，生别常恻恻。";

        _text[3] = "当其贯日月，生死安足论。";
    }

    public void LoadNextScene()
    {
        //隐藏icons
        GameObject.Find("Icons").GetComponent<IconsControl>().HideIcons();

        int index = Random.Range(0, 3);
        t.text = _text[index];
        StartCoroutine(NextScene());
    }


    private IEnumerator NextScene()
    {
        IsOperatable = false;

        _img.SetActive(true);
        t.gameObject.SetActive(true);
        _tapStyle.start = true;
        _tapStyle.isCycle = true;
        yield return new WaitForSeconds(wait);
        if (StartScene.AsyncOperation.progress < 1)
        {
            yield return new WaitUntil(() =>
                StartScene.AsyncOperation.progress >= 1f || SceneControl.Async.progress >= 1f);
        }

        //加载结束
        IsOperatable = true; //控制权限开放

        if (SceneManager.GetActiveScene().buildIndex != 0)
            GameObject.Find("Icons").GetComponent<IconsControl>().ShowIcons(); //显示图标

        //隐藏动画
        print("yincang");
        _tapStyle.start = false;
        _tapStyle.isCycle = false;
        _tapStyle.gameObject.SetActive(false);
        _img.transform.GetChild(0).gameObject.SetActive(false);
        t.gameObject.SetActive(false);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        //图片渐入
        float timer = 0;
        Color c = _img.GetComponent<Image>().color;
        while (c.a < 1)
        {
            c.a = Mathf.Lerp(0, 1, timer / 2);
            timer += Time.deltaTime;
            _img.GetComponent<Image>().color = c;
            yield return new WaitForEndOfFrame();
        }

        print("fadeover");
    }

    private IEnumerator FadeOut()
    {
        //图片溅出控制
        float timer = 0;
        Color c = _img.GetComponent<Image>().color;
        while (c.a > 0)
        {
            c.a = Mathf.Lerp(1, 0, timer / 2);
            timer += Time.deltaTime;
            _img.GetComponent<Image>().color = c;
            yield return new WaitForEndOfFrame();
        }

        print("fadeover");
        c.a = 1;
        _img.GetComponent<Image>().color = c;
        //还原动画
        _img.transform.GetChild(0).gameObject.SetActive(true);
        _tapStyle.gameObject.SetActive(true);
        _img.SetActive(false);
        t.gameObject.SetActive(true);
    }

    public void LoadStartScene()
    {
        //隐藏icons
        GameObject.Find("Icons").GetComponent<IconsControl>().HideIcons();
        
        int index = Random.Range(0, 3);
        t.text = _text[index];
        StartCoroutine(NextScene());

        GameObject.Find("Icons").GetComponent<IconsControl>().enabled = false;
    }
    
    #if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneControl.Async.allowSceneActivation = true;
            LoadNextScene();
        }
    }
    #endif
}