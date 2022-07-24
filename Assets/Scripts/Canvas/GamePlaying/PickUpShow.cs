using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品捡起展示
/// </summary>
public class PickUpShow : MonoBehaviour
{
    private List<Sprite> sprite = new List<Sprite>();
    private List<float> scale_size = new List<float>();
    private List<string> image_name = new List<string>();
    private List<string> image_describe = new List<string>();
    private bool is_show = false;
    private bool can_next = true;
    private Vector2 ori_pos;
    private int num = 0;
    private int count = 0;

    public RectTransform tar_transform;
    public Sprite sp;
    public Image bg;
    public Text text;
    public Text describe;


    private void Update()
    {
        if (!is_show)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        {
            is_show = false;
            StartCoroutine(ShowEnd());
        }
    }
    public void PickUpStart(Sprite spr,float size,string name,string describe)
    {
        count++;
        sprite.Add(spr);
        scale_size.Add(size);
        image_name.Add(name);
        image_describe.Add(describe);
        if (can_next)
        StartCoroutine(ShowStart());
    }

    IEnumerator ShowStart()
    {
        can_next = false;
        text.text = image_name[num];
        describe.text = image_describe[num];
        this.GetComponent<Image>().sprite = sprite[num];
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        ori_pos = this.GetComponent<RectTransform>().position;
        float rate = 0;
        float time = 0;
        while (rate < scale_size[num])
        {
            time += Time.deltaTime*2;
            rate = Mathf.Lerp(0, scale_size[num], time);
            this.GetComponent<RectTransform>().localScale = new Vector3(rate,rate,rate);
            text.color = new Color(1, 1, 1, rate / scale_size[num]);
            describe.color = new Color(1, 1, 1, rate / scale_size[num]);
            bg.color = new Color(0,0,0,rate/ scale_size[num]);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        is_show = true;
    }

    IEnumerator ShowEnd()
    {
        text.color = new Color(1, 1, 1, 0);
        describe.color = new Color(1, 1, 1, 0);
        bg.color = new Color(0, 0, 0, 0);
        float rate = scale_size[num];
        float time = 0;
        while (rate > 0)
        {
            time += Time.deltaTime*2;
            rate = Mathf.Lerp(scale_size[num],0 , time);
            this.GetComponent<RectTransform>().localScale = new Vector3(rate, rate, rate);
            this.GetComponent<RectTransform>().position = Vector2.Lerp(ori_pos, tar_transform.position, time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        num++;
        can_next = true;
        if(num < count)
        {
            StartCoroutine(ShowStart());
        }
        else
        {
            OperationControl.Instance.is_picking = false;
        }
    }
}
