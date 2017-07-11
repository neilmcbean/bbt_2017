using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuChapterManager : MonoBehaviour {

    private bool IsMenuSetUp = false;
    private StoryObject currentStory
    {
        get
        {
            return DataManager.currentStory;
        }
    }
    
    public GameObject ButtonTemplate;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReturnToMainMenu()
    {
        Application.LoadLevel("Menu");
    }

        public void ChapterINI()
    {
      
        if (IsMenuSetUp == false)
        {//Set up the Chapter reference in the story
            RectTransform rt = gameObject.GetComponent<RectTransform>();
            float ScrollRoomHeight = 0;
            IsMenuSetUp = true;
            int rowCounter = 0;
            float xCounter = 0;
            float yCounter = 0;
            Vector3 PositionTracker ;
            for (int audio = 0; audio < currentStory.pageObjects[0].audioObjects.Count; audio++)
            {//Create a button for each page. 
                GameObject LanguageButton = Instantiate(ButtonTemplate) as GameObject;
                LanguageButton.transform.SetParent(gameObject.transform, false);
                PositionTracker = new Vector3(LanguageButton.transform.position.x + xCounter, LanguageButton.transform.position.y - yCounter, LanguageButton.transform.position.z);
                LanguageButton.transform.position = PositionTracker;
                    if (rowCounter < 3)
                    {//If the row is incomplete, add to it
                        rowCounter++;
                        xCounter += 75;
                    }
                        else
                        {//Once the row is compelte, move down. 
                            xCounter = 0;
                            rowCounter = 0;
                            yCounter += 75;
                            ScrollRoomHeight += 115;
                            rt.sizeDelta = new Vector2(0, ScrollRoomHeight);
                        }
                LanguageButton.GetComponent<Button>().onClick.AddListener(() => gameObject.GetComponent<gotopage>());
                LanguageButton.GetComponent<gotopage>().setPageTarget(audio+1);
            }
            ButtonTemplate.SetActive(false);
        }
            else
            {//Set up which of the chapters is sellected. 

            }
                
    }
}
