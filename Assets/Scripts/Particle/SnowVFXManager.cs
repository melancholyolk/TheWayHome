using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 管理所有雪的粒子特效
/// 注意垃圾回收
/// </summary>
public class SnowVFXManager : MonoBehaviour
{
    public static bool IsAllClose = false;

    public GameObject[] snowVFXes;

    private Queue<Vector3> _initPos = new Queue<Vector3>();

    private float _tempVolume;
    public void CloseAll()
    {
        foreach (var snow in snowVFXes)
        {
            // _initPos.Enqueue(snow.transform.position);
            // snow.transform.position = Vector3.up * 100;
            if (snow.transform.childCount > 0)
            {
                var emission = snow.transform.GetChild(1).GetComponent<ParticleSystem>().emission;
                emission.enabled = false;
            }
        }

        AudioMixer mixer = GameObject.Find("WindAudio").GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
        
        mixer.GetFloat("EnvironmentVolume", out _tempVolume);
        mixer.SetFloat("EnvironmentVolume",_tempVolume -10);
    }

    public void OpenAll()
    {
        // if(_initPos.Count == 0) return;
        foreach (var snow in snowVFXes)
        {
            // snow.transform.position = _initPos.Dequeue();
            if (snow.transform.childCount > 0)
            {
                var emission = snow.transform.GetChild(1).GetComponent<ParticleSystem>().emission;
                emission.enabled = true;
            }
        }
        AudioMixer mixer = GameObject.Find("WindAudio").GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
        
        mixer.SetFloat("EnvironmentVolume",_tempVolume);
    }
}