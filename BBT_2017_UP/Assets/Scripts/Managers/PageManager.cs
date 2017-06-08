using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PageManager : Singleton<PageManager>
{
    //public static event Action<string,string> onSentenceChange;

    private StoryObject currentStory
    {
        get
        {
            return DataManager.currentStory;
        }
    }

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
    IEnumerator Start()
    {
        //Wait a frame for all scenes to be loaded

        List<TweenEvent> tweenEvents = FindObjectsOfTypeAll<TweenEvent>();
        foreach (TweenEvent evt in tweenEvents)
        {
            Register(evt);
        }

        DOTween.Init();

        //The tweens should be only be enabled AFTER DOTween has been initialized
        foreach (TweenEvent evt in tweenEvents)
        {
            evt.enabled = true;
        }

        DataManager.LoadStory(DataManager.currentStoryName);
        NextSentence();
        yield return null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !EventSystem.current.IsPointerOverGameObject())
        {
            NextSentence();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			PreviousSentence(true);
		}

        //DEBUG only
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PreviousSentence(true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PreviousSentence(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeLanguage("Spanish");
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            GoToPage(16);
        }
    }

    public void ChangeLanguage(string newLanguage)
    {
        DataManager.currentLanguage = newLanguage;
        DataManager.LoadStory(DataManager.currentStoryName);
        PreviousSentence(true);
    }

    public void Register(TweenEvent evt)
    {
        tweenEvents.Add(evt);
        evt.id = tweenEvents.Count.ToString();
    }

    public void GoToPage(int i)
    {
	StopAllCoroutines();
        sentenceContainer.Clear();
	audioIndex = i;
	PlayCurrentSentence();

    }
    
    void PreviousSentence(bool playFromLast)
    {
        StopAllCoroutines();
        sentenceContainer.Clear();
        audioIndex--;

        if (audioIndex < 0)
        {
            audioIndex = 0;

            if (pageIndex > 0)
            {
                pageIndex--;
                audioIndex = currentPage.audioObjects.Count - 1;
            }
            else
            {
                Debug.LogError("Page index can't go under 0!");
            }

        }

        AudioObject currentAudio = currentPage.audioObjects[audioIndex];
        foreach (TweenEvent evt in tweenEvents)
        {
            if (currentPage.name == evt.pageName && currentAudio.name == evt.audioName)
            {
                evt.OnDeactivate();
            }
        }

        if (playFromLast)
            NextSentence();
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
            Debug.Log("Story ended! Back to menu...");

            SceneManager.LoadScene("Menu");
            return;
        }

       PlayCurrentSentence();


    }
	void PlayCurrentSentence()
	{
 		AudioObject currentAudio = currentPage.audioObjects[audioIndex];
        	audioIndex++;

        	//Actiavte tweens
        	foreach (TweenEvent evt in tweenEvents)
        	{
          	  evt.OnNextStep();
          	  if (currentPage.name == evt.pageName && currentAudio.name == evt.audioName)
           	 {
             	   evt.OnActivate();
            	}
       	 }
      	 
      	  StartCoroutine(RunSequence(currentAudio));
	}

    IEnumerator RunSequence(AudioObject obj)
    {
        if (obj.clip != null)
        {
            audioSource.clip = obj.clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogErrorFormat("Unable to read the audio from folder {0}. " +
                "Please ensure an audio file is in the folder, and it's set to the assetbundle {1}.", obj.name, DataManager.currentStoryName);
        }

        if (obj.sentence == null)
        {
            Debug.LogErrorFormat("Unable to read the text from folder {0}. " +
                "Please ensure a text file is in the folder, and it's  set to the assetbundle {1}.", obj.name, DataManager.currentStoryName);
            yield break;
        }
            
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

    public static List<T> FindObjectsOfTypeAll<T>()
    {
        List<T> results = new List<T>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            if (s.isLoaded)
            {
                var allGameObjects = s.GetRootGameObjects();
                for (int j = 0; j < allGameObjects.Length; j++)
                {
                    var go = allGameObjects[j];
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }
        }
        return results;
    }
	
}
