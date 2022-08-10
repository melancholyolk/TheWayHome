using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI
{
    public string name;

    public bool needHideOther = false;
    public bool needBlackBg = true;
    public bool needCatchClick = false;
    public bool needSyncLoad = false;
    public bool needAnimation = false;
    public bool isFullScreen = false;
    //只执行一次
    public virtual void Ctor()
    {
        name = "BookView";
        needBlackBg = true;

    }
    //只执行一次
    public virtual void Init()
    {

	}

    public virtual void AddEvent()
    {

    }

    public virtual void OnViewBack()
    {

    }

    public virtual void OnViewBackAfter()
    {

    }

    public virtual void OnClosed()
    {

    }

    public virtual void Destory()
    {

    }
}
