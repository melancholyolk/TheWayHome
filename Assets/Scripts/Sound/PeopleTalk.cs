using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 人声低语
/// </summary>
public class PeopleTalk : MonoBehaviour
{
    public AudioClip[] talksPre;
    public int maxNumOfTalks;
    public float interval;
    public AudioMixerGroup talkMixer;
    private float _timer = 0;
    private void CreateTalk()
    {
        if(transform.childCount >= maxNumOfTalks) return;
        GameObject talk = new GameObject("Talk"+transform.childCount,typeof(AudioSource));
        talk.transform.parent = transform;
        
        var audio = talk.GetComponent<AudioSource>();
        audio.clip = talksPre[transform.childCount % talksPre.Length];
        audio.loop = true;
        audio.outputAudioMixerGroup = talkMixer;
        audio.PlayDelayed(1);
    }

    private void FixedUpdate()
    {
        if (Time.time - _timer > interval)
        {
            CreateTalk();
            _timer = Time.time;
        }
    }
}
