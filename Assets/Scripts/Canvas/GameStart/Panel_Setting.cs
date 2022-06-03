using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 控制设置面板
/// </summary>
public class Panel_Setting : MonoBehaviour
{
    public Animator ani;

    public Animator aniBK;

    public GameObject bk;

    public AudioMixerGroup bgm;

    public AudioMixerGroup sfx;

    public float volumeBGM = 0;

    public float volumeSFX = 0;

    private float _aniSpeed0,_aniSpeed1;
    // Update is called once per frame
    private void Start()
    {
        ani = GetComponent<Animator>();
        aniBK = GameObject.Find("background_setting").GetComponent<Animator>();

        _aniSpeed0 = ani.speed;
        _aniSpeed1 = aniBK.speed;
    }

   

    IEnumerator DisablePanelAfterAni()
    {
        ani.Play("ShowPanel", 0, 1);
        aniBK.Play("ShowPanel", 0, 1);
        yield return new WaitUntil(() =>
            (ani.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0 &&
             ani.GetCurrentAnimatorStateInfo(0).IsName("ShowPanel")));
        ani.StartPlayback();
        ani.speed = _aniSpeed0;
        aniBK.StartPlayback();
        aniBK.speed = _aniSpeed1;
        transform.parent.SendMessage("HideSetting");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        bk.SetActive(true);
        ani.Play("ShowPanel", 0, 0);
        aniBK.Play("ShowPanel", 0, 0);
        bgm.audioMixer.GetFloat("BGMVolume", out volumeBGM);
        transform.GetChild(3).GetComponent<Slider>().value = volumeBGM;

        sfx.audioMixer.GetFloat("SfXVolume", out volumeSFX);
        transform.GetChild(5).GetComponent<Slider>().value = volumeSFX;
    }

    public void Hide()
    {
        ani.StartPlayback();
        ani.speed = -_aniSpeed0;
        aniBK.StartPlayback();
        aniBK.speed = -_aniSpeed1;
        StartCoroutine(DisablePanelAfterAni());
    }

    public void HideImmediately()
    {
        bk.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ChangeBGMVolume()
    {
        float temp = volumeBGM;

        volumeBGM = transform.GetChild(3).GetComponent<Slider>().value;
        bgm.audioMixer.SetFloat("BGMVolume", volumeBGM);

        ChangeVolumeSprite("Text_BGM", temp, volumeBGM);
    }

    public void ChangeSFXVolume()
    {
        float temp = volumeSFX;
        volumeSFX = transform.GetChild(5).GetComponent<Slider>().value;
        sfx.audioMixer.SetFloat("SfXVolume", volumeSFX);

        ChangeVolumeSprite("Text_SFX", temp, volumeSFX);
    }

    private void ChangeVolumeSprite(string name, float oldVolume, float newVolume)
    {
        if (newVolume <= -80 && oldVolume > -80)
            GameObject.Find(name).transform.GetChild(0).SendMessage("SetSprite", "none");
        else if (newVolume >= 0 && oldVolume < 0)
        {
            GameObject.Find(name).transform.GetChild(0).SendMessage("SetSprite", "normal");
        }
        else if (newVolume < 0 && (oldVolume <= -80 || oldVolume >= 0))
        {
            GameObject.Find(name).transform.GetChild(0).SendMessage("SetSprite", "little");
        }
    }
}