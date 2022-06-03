using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TodoUnit : MonoBehaviour
{
    public string taskID;
    
    public Text text;

    public Mask mask;

    public bool isDone = false;

    [SerializeField] private float _length;

    // Start is called before the first frame update
    void Start()
    {
    }

    private float CalcTextWidth(Text text)
    {
        TextGenerator tg = text.cachedTextGeneratorForLayout;
        TextGenerationSettings setting = text.GetGenerationSettings(Vector2.zero);
        float width = tg.GetPreferredWidth(text.text, setting) / text.pixelsPerUnit;
        return width;
    }

    public void SetText(string content)
    {
        text = transform.GetChild(0).GetComponent<Text>();
        text.text = content;

        _length = CalcTextWidth(text);

        SetMask();
    }

    private void SetMask()
    {
        mask = transform.GetChild(1).GetComponent<Mask>();

        mask.transform.position = text.transform.position;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        mask.transform.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _length*2);
    }

    public void IsDone()
    {
        GetComponent<Animator>().Play("OndoneHide", 0, 0f);
        StartCoroutine(PlayImhAnimation());
    }

    private IEnumerator PlayImhAnimation()
    {
        float timer = 0;
        float currentwidth = mask.rectTransform.rect.width;
        Color color = text.color;
        while (mask.rectTransform.rect.width - _length <= -0.01f)
        {
            currentwidth = Mathf.Lerp(currentwidth, _length, timer);
            color = Color.Lerp(color, Color.grey, timer);
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentwidth);
            text.color = color;
            mask.GetComponentInChildren<Text>().color = color;
            timer += Time.deltaTime * 0.8f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(0.3f);
        isDone = true;
        gameObject.SetActive(false);
    }
}