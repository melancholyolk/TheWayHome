using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 打字机效果
/// </summary>
public class TextTapStyle : MonoBehaviour
{
    public string sentence;//要显示的句子

    public int pointer;//字符指针

    public float interval;//效果延迟

    public bool start;//是否开始

    public bool isCycle;//是否循环

    private float _timer = 0;
    
    private Text _text;

    private int _length;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _length = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            _timer += Time.deltaTime;
            if (_timer >= interval)
            {
                _timer = 0;
                if (++_length <= sentence.Length - pointer)
                {
                    _text.text = sentence.Substring(0,pointer)+sentence.Substring(pointer,_length);
                }
                else
                {
                    if (isCycle)
                    {
                        _length = 0;
                    }
                }
            }
        }
        
    }
}
