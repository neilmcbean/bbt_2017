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
    public static Language language;

    internal AssetBundle myLoadedAssetBundle;
    public string storyName;

    public StoryObject LoadStory()
    {
        UnloadStory();
        myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "english/" + storyName));


        if (myLoadedAssetBundle == null)
        {
            Debug.LogErrorFormat("Failed to load AssetBundle from story {0}", storyName);
            return null;
        }
        StoryObject story = new StoryObject();

        
        string[] files = myLoadedAssetBundle.GetAllAssetNames();
        foreach (string file in files)
        {
            AddFileToStory(story, file); 
        }

        return story;
    }

    public void UnloadStory()
    {
        if (myLoadedAssetBundle != null)
        {
            myLoadedAssetBundle.Unload(true);
            myLoadedAssetBundle = null;
        }
    }

    private  void AddFileToStory(StoryObject story, string file)
    {
        string[] splitPath = file.Split('/');
        for (int i = 0; i < splitPath.Length; i++)
        {
            if (splitPath[i] == storyName)
            {
                if (i + 2 >= splitPath.Length)
                {
                    Debug.LogWarningFormat("Can't add file to story {0}", file);
                    return;
                }
                string pageName = splitPath[i + 1];
                //Get the page from the story
                PageObject page = story.GetPage(pageName);
                //If the page wasn't in the story yet, create a new object
                if (page == null)
                {
                    page = new PageObject()
                    {
                        name = pageName
                    };
                    story.pageObjects.Add(page);  
                }
                //Get the audio from the page. The name is actually the foldername, not the file name of the audio
                string audioName = splitPath[i + 2];
                //If the audio doesn't exist, craete a new one
                AudioObject audioObj = page.GetAudio(audioName);
                if (audioObj == null)
                {
                    audioObj = new AudioObject()
                    {
                        name = audioName
                    };
                    page.audioObjects.Add(audioObj);
                }
                Object fileObj = myLoadedAssetBundle.LoadAsset(file);
                AudioClip clip = fileObj as AudioClip;
                if (clip != null)
                {
                    audioObj.clip = clip;
                    return;
                }
                TextAsset txt = fileObj as TextAsset;
                if (txt != null)
                {
                    audioObj.sentence = GetSentence(txt.text);
                    return;
                }
                Debug.LogErrorFormat("File of type {0} detected inside the AssetBundle. It only supports AudioClips and TextAssets!", fileObj.GetType());

                return;
            }
        }
    }

    private SentenceObject GetSentence(string dataString)
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
    Spanish,
    French
}
