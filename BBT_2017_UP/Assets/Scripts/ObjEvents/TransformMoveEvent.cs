using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransformMoveEvent : TweenEvent
{
    public Transform targetObj;
    public float tweenDuration;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnNextStep()
    {
        
    }

    public override void OnActivate()
    {
        targetObj.DOMove(transform.position, tweenDuration);
        targetObj.DORotate(transform.position, tweenDuration);
    }

   
}
