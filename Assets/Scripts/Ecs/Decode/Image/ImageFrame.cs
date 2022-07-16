using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Decode;
public class ImageFrame : MonoBehaviour
{
    public float angle = 0;
    public bool is_rotate = false;
    private float ori = 1;

    public bool is_right = false;
    public float size = 0.5f;
    private void Start()
    {
        ori = 1;
        if (angle == 0)
        {
            is_right = true;
        }
        else
        {
            is_right = false;
        }
    }
    public void FrameChoosed(bool state)
    {
        if (state)
        {
            StartCoroutine(ChangeBig());
        }
        else
        {
            StartCoroutine(ChangeSmool());
        }
    }

    public void FrameRotate()
    {
        if (is_rotate)
        {
            return;
        }
        else
        {
            StartCoroutine(StartRotate());
        }
    }

    IEnumerator ChangeBig()
    {
        float rate = size / ori;
        float time = 0;
        while (rate < size * 1.2f / ori)
        {
            time += Time.deltaTime*5;
            rate = Mathf.Lerp(size / ori, size * 1.2f / ori , time);
            this.transform.localScale = new Vector3(rate,rate,rate);
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }

    IEnumerator ChangeSmool()
    {
        float rate = size * 1.2f / ori;
        float time = 0;
        while (rate > size/ ori)
        {
            time += Time.deltaTime * 5;
            rate = Mathf.Lerp(size * 1.2f / ori, size / ori, time);
            this.transform.localScale = new Vector3(rate, rate, rate);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator StartRotate()
    {
        is_rotate = true;
        float ori_rotate = angle;
        float rate = ori_rotate;
        float time = 0;
        while (rate < ori_rotate + 90)
        {
            time += Time.deltaTime * 5;
            rate = Mathf.Lerp(ori_rotate, ori_rotate + 90, time);
            this.transform.eulerAngles = new Vector3(0, 0, rate);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        angle = (angle + 90) % 360;
        if(angle == 0)
        {
            is_right = true;
        }
        else
        {
            is_right = false;
        }
        is_rotate = false;
        this.transform.parent.GetComponent<ImageController>().Judge();
    }
}
