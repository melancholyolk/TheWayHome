using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerControl : MonoBehaviour
{
    private Image image;
    public Sprite sprite;
    public Text text;
    public GameObject input;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void Complete()
    {
        image.sprite = sprite;
        input.active = false;
    }

    public void Wrong()
    {
        text.text = "";
    }
}
