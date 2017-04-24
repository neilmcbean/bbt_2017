using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
   
    public string highlightOpeningTag;
    public string highlightClosingTag;

    private SceneAudioObject sceneObj;
    private int audioIndex;

    private AudioSource audioSource;
    public SentenceRowContainer sentenceContainer;


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

        }
    }

    void NextSentence()
    {
        AudioObject currentAudio = sceneObj.audioObjects[audioIndex];
        audioIndex++;
        StartCoroutine(RunSequence(currentAudio));
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

        while (i < obj.sentence.wordGroups.Count)
        {
            WordGroupObject wordGroup = obj.sentence.wordGroups[i];
            sentenceContainer.HighlightWordGroup(wordGroup);
            i++;
            yield return new WaitForSeconds(wordGroup.time);
        }
        sentenceContainer.HighlightWordGroup(null);

    }
	
}
