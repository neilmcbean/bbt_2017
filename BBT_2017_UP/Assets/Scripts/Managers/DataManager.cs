using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;

public class DataManager : Singleton<DataManager>
{
    public Language language;

    public SceneAudioObject LoadSceneData()
    {
        string sceneName = gameObject.scene.name;
        SceneAudioObject obj = new SceneAudioObject();

        string currentFolder = language.ToString() + "/" + sceneName;

        string[] folders = Directory.GetDirectories(Application.dataPath + "/Resources/" + currentFolder);
        for (int i = 0; i < folders.Length; i++)
        {
            AudioObject audioObj = new AudioObject()
            {
                clip = Resources.LoadAll<AudioClip>(currentFolder)[i],
                sentence = GetSentence(Resources.LoadAll<TextAsset>(currentFolder)[i].text)
            };
            obj.audioObjects.Add(audioObj);
           
        }
        return obj;
    }

    public SentenceObject GetSentence(string dataString)
    {
        return JsonUtility.FromJson<SentenceObject>(dataString);
    }
}

public enum Language
{
    English,
    Spanish
}
