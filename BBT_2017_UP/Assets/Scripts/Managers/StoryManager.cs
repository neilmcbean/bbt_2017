using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour {

	public int AudioIndexPosition; 
	public string LevelName;
	public int pagesPerScene;
	public string NextScene;
	public string LastScene;
	public bool isLastscene;
	public bool isFirstscene;
	public bool isLoadingLevel;
	public int StreamingAssetsCounter;
	public GameObject PageManager;
	private GameObject Canvas;

	// Use this for initialization
	void Start () {

		//Set references 
		PageManager = GameObject.FindGameObjectWithTag ("PageManager");
		Canvas = GameObject.FindGameObjectWithTag ("Canvas");

		if (isLoadingLevel == true) {//If this is going to load a different streaming package, load it here. 
			
			PageManager.GetComponent<PageManager>().audioIndex = 0;
			DataManager.LoadStory (DataManager.currentStoryName, StreamingAssetsCounter.ToString());
		}
			else if (StreamingAssetsCounter.ToString() != DataManager.CurrentAssetPackage.ToString()) 
			{//this condition will trigger when the player loads a level from the menu that requires a streaming asset that is not currently loaded.
			DataManager.LoadStory (DataManager.currentStoryName, StreamingAssetsCounter.ToString());
			}

		//This variable loads the offset from the page manager so that the level starts off at the right passage.
		int chapterOffset = PageManager.GetComponent<PageManager> ().ChapterOffSet;

		for(int i = 0; i < Canvas.transform.GetChildCount(); i++)
		{//This loop generates the book mark dots 
			if (Canvas.transform.GetChild (i).name == "UI Dots") {
				Canvas.transform.GetChild (i).GetComponent<DotGenerator> ().GenrateTheDots (pagesPerScene);
			}
		}

		if (PageManager.GetComponent<PageManager> ().isGoingBack == false) {
			//Set up the narrative variables
			PageManager.GetComponent<PageManager> ().AssetAssigner (LevelName, AudioIndexPosition+chapterOffset);
			PageManager.GetComponent<PageManager> ().GoToPage (AudioIndexPosition+chapterOffset);
			PageManager.GetComponent<PageManager> ().ChapterskipSetCharacters(chapterOffset);
			} 
				else 
				{//If the player is going backwards
				//Debug.Log ("isGoingBack="+PageManager.GetComponent<PageManager> ().isGoingBack);
				//PageManager = GameObject.FindGameObjectWithTag ("PageManager");
				PageManager.GetComponent<PageManager> ().AssetAssigner (LevelName,pagesPerScene-1);
				PageManager.GetComponent<PageManager> ().GoToPage (AudioIndexPosition+(pagesPerScene)-1);	
				PageManager.GetComponent<PageManager> ().isGoingBack = false;
				}
	PageManager.GetComponent<PageManager> ().ChapterOffSet = 0;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
