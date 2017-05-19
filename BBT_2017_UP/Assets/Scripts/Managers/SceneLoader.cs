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
            string sceneName = DataManager.currentStoryName + scenePath;
            Scene s = SceneManager.GetSceneByName(sceneName);
                
            if (!s.isLoaded)
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
	
}
