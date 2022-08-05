using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 控制设置面板
/// </summary>
public class Panel_Setting : MonoBehaviour
{
	public DistinguishSetting distinguish;
	
    public Animator ani;

    public Animator aniBK;

    public GameObject bk;

    public AudioMixerGroup bgm;

    public AudioMixerGroup sfx;

    public Setting setting;
	    
    private float volumeBGM = 0;

    private float volumeSFX = 0;

    [FormerlySerializedAs("Setting")] 
    public GameSetting gameSetting;

    public VolumePicture volumePicture;

    public Slider volumeSlider;
    private float _aniSpeed0,_aniSpeed1;
    // Update is called once per frame
    public void Start()
    {
        ani = GetComponent<Animator>();
        _aniSpeed0 = ani.speed;
        _aniSpeed1 = aniBK.speed;
        volumeBGM = gameSetting.volume;
        
        InitSetting();
    }

    public void InitSetting()
    {
	    SetVolume();
	    distinguish.SetDistinguish();
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
        setting.HideSetting();
        SceneTransition.IsOperatable = true;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        bk.SetActive(true);
        ani.Play("ShowPanel", 0, 0);
        aniBK.Play("ShowPanel", 0, 0);
        bgm.audioMixer.GetFloat("BGMVolume", out volumeBGM);
       volumeSlider.value = volumeBGM;

        // sfx.audioMixer.GetFloat("SfXVolume", out volumeSFX);
        // transform.GetChild(5).GetComponent<Slider>().value = volumeSFX;
    }

    public void Hide()
    {
	    SceneTransition.IsOperatable = false;
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

        volumeBGM = volumeSlider.value;
        bgm.audioMixer.SetFloat("BGMVolume", volumeBGM);

        ChangeVolumeSprite("Text_BGM", volumeBGM);
        //序列化
        gameSetting.volume = volumeBGM;
    }

    public void ChangeSFXVolume()
    {
        float temp = volumeSFX;
        volumeSFX = transform.GetChild(5).GetComponent<Slider>().value;
        sfx.audioMixer.SetFloat("SfXVolume", volumeSFX);

        ChangeVolumeSprite("Text_SFX", volumeSFX);
    }

    public void SetVolume()
    {
	    bgm.audioMixer.SetFloat("BGMVolume", volumeBGM);

	    ChangeVolumeSprite("Text_BGM", volumeBGM);
    }
    private void ChangeVolumeSprite(string name, float newVolume)
    {
	    if (newVolume <= -80)
            volumePicture.SetSprite("none");
        else if (newVolume >= 0 )
        {
	        volumePicture.SetSprite("normal");
        }
        else if (newVolume < 0)
        {
	        volumePicture.SetSprite("little");
        }
    }
}