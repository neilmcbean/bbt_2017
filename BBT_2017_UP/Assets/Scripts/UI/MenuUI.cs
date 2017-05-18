using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Dropdown languageDropdown;

    void Awake()
    {
        languageDropdown.onValueChanged.AddListener(OnDropDownChanged);
    }

    void OnDropDownChanged(int i)
    {
        DataManager.currentLanguage = languageDropdown.options[i].text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainNav");
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
