using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    string[] scenePaths = new string[]
    {
        "_characters",
        "_environments"
    };

    void Awake()
    {
        LoadScenes();
    }

    void LoadScenes()
    {
        foreach (string scenePath in scenePaths)
        {
            string sceneName = DataManager.storyName + scenePath;
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
	
}
