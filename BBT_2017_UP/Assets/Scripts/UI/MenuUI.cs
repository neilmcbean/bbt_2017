using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;

public class MenuUI : MonoBehaviour
{
    
    public void OnFrenchButton()
    {
        DataManager.language = Language.French;
        StartGame();
    }

    public void OnEnglishButton()
    {
        DataManager.language = Language.English;
        StartGame();
    }

    void StartGame()
    {
        SceneManager.LoadScene("TestScene");
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
