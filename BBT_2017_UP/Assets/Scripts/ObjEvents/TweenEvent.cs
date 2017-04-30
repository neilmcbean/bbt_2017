using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenEvent : BaseEvent
{
    public DOTweenAnimation tween;

    protected override void OnActivation()
    {
        tween.DOPlayForward();
    }
}
