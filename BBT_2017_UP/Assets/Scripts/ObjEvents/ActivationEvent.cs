using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationEvent : BaseEvent
{
    public GameObject obj;
    public bool activate;


    protected override void OnActivation()
    {
        obj.SetActive(activate);
    }

}
