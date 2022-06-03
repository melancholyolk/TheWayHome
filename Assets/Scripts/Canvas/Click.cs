using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerClickHandler
{

    public UnityEvent leftClick;
    public UnityEvent rightClick;

    internal void AddListener(Action onClick)
    {
        throw new NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }

    private void ButtonRightClick()
    {
        throw new NotImplementedException();
    }

    private void ButtonLeftClick()
    {
        throw new NotImplementedException();
    }

}

