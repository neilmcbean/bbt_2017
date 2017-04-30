using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//Bit of a hack. Every event inherit from TweenEvent, but they don't all Tween.
public class TweenEvent : DOTweenAnimation
{

    [HideInInspector]
    public string pageName;
    [HideInInspector]
    public string audioName;

    protected virtual void Awake()
    {
        PageManager.instance.Register(this);

    }

    public virtual void Activate()
    {
        DOPlayForward();
    }
}
