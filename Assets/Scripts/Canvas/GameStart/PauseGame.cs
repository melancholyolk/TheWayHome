using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class PauseGame : MonoBehaviour
{
    public AudioMixer stopBGM;

    public Sprite stop;

    public Sprite run;

    [SerializeField] private float bgmVolume, sfxVolume;

    public void ChangeState()
    {
        bool isOn = GetComponent<Toggle>().isOn;
        print("CHnageState" + isOn);
        if (isOn)
        {
            Pause();
            GetComponent<Image>().sprite = run;
        }
        else
        {
            Run();
            GetComponent<Image>().sprite = stop;
        }
    }

    private void Pause()
    {
        if (SceneTransition.IsPaused) return;
        print("PAUSE");
        Time.timeScale = 0;
        // stopBGM.GetFloat("BGMVolume", out bgmVolume);
        // stopBGM.SetFloat("BGMVolume", -80);
        stopBGM.GetFloat("SFXVolume", out sfxVolume);
        stopBGM.SetFloat("SFXVolume", -80);
        Camera.main.GetComponent<AudioSource>().Pause();
        SceneTransition.IsPaused = true;
    }

    private void Run()
    {
        if (!SceneTransition.IsPaused) return;
        print("RUN");
        Time.timeScale = 1;
        // stopBGM.SetFloat("BGMVolume", bgmVolume);
        stopBGM.SetFloat("SFXVolume", sfxVolume);
        Camera.main.GetComponent<AudioSource>().Play();
        SceneTransition.IsPaused = false;
    }
}