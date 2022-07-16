using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVLetter : MonoBehaviour
{
    public Sprite[] sprites;

    public int count = 0;

    private SpriteRenderer image;

    private void Start()
    {
        image = GetComponent<SpriteRenderer>();
        ShowLetter();
    }
    public void TVAdd()
    {
        count = (count + 1) % sprites.Length;
        ShowLetter();
    }

    public void TVSub()
    {
        if(count == 0)
        {
            count = sprites.Length-1;
        }
        else
        {
            count--;
        }
        ShowLetter();
    }

    private void ShowLetter()
    {
        image.sprite = sprites[count];
    }

}
