﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using DG.Tweening.Plugins.Options;

public class MenuUI : MonoBehaviour
{
    public Dropdown languageDropdown;
    public Dropdown storyDropdown;

    //TODO Dry this code up
    void Awake()
    {
        DataManager.languageManager = new string[languageDropdown.options.Count];

        for (int i = 0; i < languageDropdown.options.Count; i++)
        {
            DataManager.languageManager[i] = languageDropdown.options[i].text.ToLower();
            //Debug.Log(languageDropdown.options[i].text);
            if (languageDropdown.options[i].text.ToLower() == DataManager.currentLanguage)
            {
                languageDropdown.value = i;
            }
        }
        languageDropdown.onValueChanged.AddListener(OnLanguageDropdownChanged);

        for (int i = 0; i < storyDropdown.options.Count; i++)
        {
            if (storyDropdown.options[i].text.ToLower() == DataManager.currentStoryName)
            {
                storyDropdown.value = i;
            }
        }
        storyDropdown.onValueChanged.AddListener(OnStoryDropdownChanged);
    }

    void OnLanguageDropdownChanged(int i)
    {
        DataManager.currentLanguage = languageDropdown.options[i].text.ToLower();
        Debug.Log(DataManager.currentLanguage);
    }

    void OnStoryDropdownChanged(int i)
    {
        DataManager.currentStoryName = storyDropdown.options[i].text.ToLower();
		//Debug.Log(DataManager.currentStoryName);
    }

    public void StartGame()
    {
        //Debug.Log("Working");
        SceneManager.LoadScene("MainStory");
    }

    public void OnPurchaseSuccess(Product p)
    {
        Debug.LogFormat("Yayyy. You just purchased {0}", p.definition.id);
    }

    public void OnPurchaseFailed(Product p, PurchaseFailureReason failReason)
    {
        Debug.LogFormat("Failed to purchase {0}. Reason: {1}", p.definition.id, failReason);
    }
}
