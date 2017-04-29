using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(BaseEvent), true)]
public class EventEditor : Editor
{

    private static StoryObject story;

    public override void OnInspectorGUI()
    {
        
        BaseEvent baseEvent = (BaseEvent)target;

        if (DataManager.instance == null)
        {
            GUILayout.Label("No DataManager Singleton found in scene");
            return;
        }
        if (story == null)
            story = DataManager.instance.LoadStory();

        if (story == null)
        {
            Debug.LogErrorFormat("Story with the name {0} could not be loaded!", DataManager.instance.storyName);
            return;
        }


        //We get the index based on the name
        int index = 0;
        for (int i = 0; i < story.pageObjects.Count; i++)
        {
            if (baseEvent.pageName == story.pageObjects[i].name)
            {
                index = i;
                break;
            }
        }

        string[] pageNames = story.GetPageNames();
        index = EditorGUILayout.Popup("Page", index, pageNames); 

        baseEvent.pageName = pageNames[index];

        PageObject page = story.GetPage(baseEvent.pageName);

        //We get the index based on the name
        index = 0;
        for (int i = 0; i < page.audioObjects.Count; i++)
        {
            if (baseEvent.audioName == page.audioObjects[i].name)
            {
                index = i;
                break;
            }
        }

        string[] audioNames = page.GetAudioNames();
        index = EditorGUILayout.Popup("Audio", index, audioNames); 

        baseEvent.audioName = audioNames[index];
        base.OnInspectorGUI();
    }
}
