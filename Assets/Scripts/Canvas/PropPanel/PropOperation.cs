using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 道具栏点击及物品显示
/// </summary>
public class PropOperation : Click, IPointerExitHandler
{
    public Text name;
    public PropInfo prop_info = new PropInfo();
    public Image prop_image;
    public GameObject text_panel;
    public GameObject info_panel;
    public bool can_use = true;

    public void SetPanelInfo(PropInfo prop)
    {
        prop_info = prop;
        prop_image.sprite = prop_info.prop_sprite;
        can_use = false;
    }

    void Start()
    {
        leftClick.AddListener(new UnityAction(ButtonLeftClick));
        rightClick.AddListener(new UnityAction(ButtonRightClick));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        info_panel.SetActive(false);
    }

    private void ButtonLeftClick()
    {
        if (prop_info.prop_name == "")
        {
            return;
        }

        info_panel.SetActive(true);
        info_panel.GetComponent<RectTransform>().sizeDelta = new Vector2(prop_info.prop_name.Length * 15 + 10, 30);
        text_panel.GetComponent<Text>().text = prop_info.prop_name;
        text_panel.GetComponent<RectTransform>().sizeDelta = new Vector2(prop_info.prop_name.Length * 15, 30);
    }

    private void ButtonRightClick()
    {
        CanvasManager.Instance.DiscarderProp(prop_info);
        prop_info = new PropInfo();
        can_use = true;
        prop_image.sprite = null;
    }

    public void InitPanel()
    {
        prop_info = new PropInfo();
        can_use = true;
        prop_image.sprite = null;
    }
}