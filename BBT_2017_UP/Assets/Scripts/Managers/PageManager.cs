using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PageManager : Singleton<PageManager>
{
    //public static event Action<string,string> onSentenceChange;

    private StoryObject currentStory;

    private PageObject currentPage
    {
        get
        {
            return currentStory.pageObjects[pageIndex];
        }
    }

    private int pageIndex;
    private int audioIndex;

    private AudioSource audioSource;
    private SentenceRowContainer sentenceContainer;

    private List<TweenEvent> tweenEvents = new List<TweenEvent>();


    protected override void Awake()
    {
        base.Awake();
    
        sentenceContainer = FindObjectOfType<SentenceRowContainer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        currentStory = DataManager.instance.LoadStory();
        NextSentence();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextSentence();
        }
    }

    public void Register(TweenEvent evt)
    {
        tweenEvents.Add(evt);
        evt.id = tweenEvents.Count.ToString();
    }

    void NextSentence()
    {
        StopAllCoroutines();
        sentenceContainer.Clear();

        if (audioIndex >= currentPage.audioObjects.Count)
        {
            pageIndex++;
            audioIndex = 0;
        }

        if (pageIndex >= currentStory.pageObjects.Count)
        {
            Debug.Log("Story ended! Restarting...");

            //We have to unload our asset, since we can't load it twice
            DataManager.instance.UnloadStory();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        AudioObject currentAudio = currentPage.audioObjects[audioIndex];
        audioIndex++;

        //Actiavte tweens
        foreach (TweenEvent evt in tweenEvents)
        {
            if (currentPage.name == evt.pageName && currentAudio.name == evt.audioName)
            {
                evt.Activate();
            }
        }
       
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
