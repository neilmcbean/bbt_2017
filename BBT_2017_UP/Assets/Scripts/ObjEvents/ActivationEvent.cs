using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationEvent : TweenEvent
{
    public bool activate;

    public override void OnActivate()
    {
        gameObject.SetActive(activate);
    }

}
