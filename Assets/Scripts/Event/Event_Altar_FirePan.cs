using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 祭坛火盆
/// </summary>
public class Event_Altar_FirePan : Event_FirePan
{
    public int order;
    public Event_Altar altar;
    private ParticleSystem.EmissionModule _emissionModule;
    private Light _light;
    private AudioSource _audioSource;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _emissionModule = _fire.emission;
        _light = GetComponentInChildren<Light>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        TurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanvasManager.Instance.CanOperate())
        {
            if (canUse)
            {
                if (JudgeCondition())
                {
                    EventOver();
                }
                else
                {
                    if (_notTriggerDialog.Count > 0)
                    {
                        canvas.GetComponent<View_Control>().ShowDialog(_notTriggerDialog);
                        return;
                    }
                }
            }
        }
    }

    private void EventOver()
    {
        if (_triggerDialog.Count > 0)
        {
            canvas.GetComponent<View_Control>().ShowDialog(_triggerDialog);
            TurnOn();
            altar.CheckFire(order);
        }
    }

    public void TurnOff()
    {
        _emissionModule.enabled = false;
        _light.enabled = false;
        _animator.SetTrigger("hide");
        _audioSource.Stop();
    }

    private void TurnOn()
    {
        _emissionModule.enabled = true;
        _light.enabled = true;
        _animator.SetTrigger("show");
        _audioSource.Play();
    }
}