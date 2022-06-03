using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制音量图片显示
/// </summary>
public class VolumePicture : MonoBehaviour
{
    public Sprite noneSprite;
    public Sprite littleSprite;
    public Sprite normalSprite;

    public void SetSprite(string name)
    {
        Image img = GetComponent<Image>();
        switch (name)
        {
            case "none":
                img.sprite = noneSprite;
                break;
            case "little":
                img.sprite = littleSprite;
                break;
            case "normal":
                img.sprite = normalSprite;
                break;
        }
    }
}
