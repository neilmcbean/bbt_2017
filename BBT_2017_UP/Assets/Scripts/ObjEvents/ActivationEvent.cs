using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationEvent : TweenEvent
{
    public bool activate;


    public override void Activate()
    {
        gameObject.SetActive(activate);
    }

}
