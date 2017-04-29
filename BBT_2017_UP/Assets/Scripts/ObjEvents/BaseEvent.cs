using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection.Emit;

public abstract class BaseEvent : MonoBehaviour
{
    [HideInInspector]
    public string pageName;
    [HideInInspector]
    public string audioName;

    protected virtual void OnEnable()
    {
        PageManager.onSentenceChange += PageManager_onSentenceChange;
    }


    protected virtual void OnDisable()
    {
        PageManager.onSentenceChange -= PageManager_onSentenceChange;
    }

    private void PageManager_onSentenceChange(string pageName, string textName)
    {
        if (this.pageName == pageName && this.audioName == textName)
        {
            OnActivation();
        }
    }

    protected abstract void OnActivation();
}