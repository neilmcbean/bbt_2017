using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenEvent : BaseEvent
{

    protected override void OnActivation()
    {
        DOTweenAnimation[] animations = GetComponents<DOTweenAnimation>();
        foreach (DOTweenAnimation anim in animations)
        {
            anim.DOPlayForward();
        }
    }
}
