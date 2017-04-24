using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PageManager : MonoBehaviour
{

    private SceneAudioObject sceneObj;
    private int audioIndex;

    private AudioSource audioSource;
    private SentenceRowContainer sentenceContainer;


    void Awake()
    {
        sentenceContainer = FindObjectOfType<SentenceRowContainer>();
        audioSource = GetComponent<AudioSource>();

    }

    // Use this for initialization
    void Start()
    {
        sceneObj = DataManager.instance.LoadSceneData();
        NextSentence();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextSentence();
        }
    }

    void NextSentence()
    {
        sentenceContainer.Clear();
        if (audioIndex < sceneObj.audioObjects.Count)
        {
            AudioObject currentAudio = sceneObj.audioObjects[audioIndex];
            audioIndex++;
            StartCoroutine(RunSequence(currentAudio));
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator RunSequence(AudioObject obj)
    {
        audioSource.clip = obj.clip;
        audioSource.Play();
        //Displaying all words in the bottom
        foreach (WordGroupObject wordGroup in obj.sentence.wordGroups)
        {
            sentenceContainer.AddText(wordGroup);
        }

        //highlight the proper wordgroups
        int i = 0;
        WordGroupObject prevWordGroup = null;

        while (i < obj.sentence.wordGroups.Count)
        {
            WordGroupObject wordGroup = obj.sentence.wordGroups[i];
            sentenceContainer.HighlightWordGroup(wordGroup);
            i++;
            //We calculate it like this because the times given are actually absolute times, not times per word
            float waitTime = wordGroup.time;
            if (prevWordGroup != null)
            {
                waitTime -= prevWordGroup.time;
            }
            yield return new WaitForSeconds(waitTime);
            prevWordGroup = wordGroup;
        }
        sentenceContainer.HighlightWordGroup(null);


    }
	
}
