using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;
using System.Net.Sockets;
using System.Text.RegularExpressions;

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
        SentenceObject so = new SentenceObject();
        string[] lines = Regex.Split(dataString, "\\n");
        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line))
                continue;
            
            string[] words = Regex.Split(line, "\\t");
            WordGroupObject obj = new WordGroupObject();
            float.TryParse(words[0], out obj.time);
            //This is index 1 or 2 (dependend if the time is defined twice or not)
            obj.text = words[words.Length - 1];
            so.wordGroups.Add(obj);
        }
        return so;
        //return JsonUtility.FromJson<SentenceObject>(dataString);
    }
}

public enum Language
{
    English,
    Spanish
}
