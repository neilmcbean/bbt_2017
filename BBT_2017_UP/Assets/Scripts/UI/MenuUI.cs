using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
