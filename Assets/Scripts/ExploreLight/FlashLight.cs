using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 灯光闪烁效果
/// 挂在灯光上
/// </summary>
public class FlashLight : MonoBehaviour
{
    public float interval;//灯光间隔时间

    public AnimationCurve animationCurve;
    private Light _light;

    private float _timer = 0;

    private float _interval;

    private float init;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        _interval = interval;
        init = _light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _interval)
        {
            // _interval = Random.Range(5, interval);
            _timer = 0;
            StartCoroutine(Flash());
        }
    }

    IEnumerator Flash()
    {
        float timer = 0;
        while (timer < _interval)
        {
            _light.intensity = animationCurve.Evaluate(timer)*init;
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
