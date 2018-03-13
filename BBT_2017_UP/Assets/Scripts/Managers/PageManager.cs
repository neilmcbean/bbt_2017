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

	private StoryObject currentStory {
		get {
			return DataManager.currentStory;
			Debug.Log (currentStory);
		}
	}

	private PageObject currentPage {
		get {
			return currentStory.pageObjects [pageIndex];
		}
	}
	//PageKeepers
	private int pageIndex;
	private int audioIndex;
	public int sceneindex;
	/// <summary>/// /////TURN THIS PRIVATE/// </summary>
	private int LastPageLoader;
	public bool isGoingBack = false;
	public int ChapterOffSet = 0;

	//Technicals
	private AudioSource audioSource;
	private SentenceRowContainer sentenceContainer;
	private bool isForward = true;
	private List<TweenEvent> tweenEvents = new List<TweenEvent> ();
	public GameObject[] Characters;
	public GameObject[] DynamicProps;

	//Narrative Manager vars
	public GameObject StoryManager;
	private string EnvironmentTracker;
	public GameObject TextBody;
	private Vector3 OG_PostitionTextBody;

	//Camera Variables
	private Vector3 cameraPreviousPosition;
	public  Transform cameraTransformTracker;
	private bool isCamMoving = false;

	//Menu Variables
	public bool isMenuDeployed = false;

	//Mouse Tracking Variabels
	private bool CanSwipe = true;
	private  Vector2 mouseStartPosition;
	private  float mouseDistance;
	private  float mouseoffset;
	private  bool isMouseMoving;
	private  Vector2 mouseEndPosition;
	private GameObject MountainTest;
	private GameObject CharacterCoin;
	private string Speaker;
	private int narrativeCounter = 1;

	//UI Assets
	public GameObject UIDots;
	public GameObject ScentenceContainer;

	//Debuging Vars
	public GameObject Scenetext;



	protected override void Awake ()
	{
		base.Awake ();
		sentenceContainer = FindObjectOfType<SentenceRowContainer> ();
		audioSource = GetComponent<AudioSource> ();
	}

    
	// Use this for initialization
	IEnumerator Start ()
	{

		//Debug.Log(DataManager.currentStoryName);
		AssetAssigner (DataManager.currentStoryName + "_start", 11);
		DataManager.LoadStory (DataManager.currentStoryName);

		OG_PostitionTextBody = TextBody.gameObject.transform.position;
		/*
		List<TweenEvent> tweenEvents = FindObjectsOfTypeAll<TweenEvent> ();
		foreach (TweenEvent evt in tweenEvents) {
			Register (evt);
		}
		DOTween.Init ();
		//The tweens should be only be enabled AFTER DOTween has been initialized
		foreach (TweenEvent evt in tweenEvents) {
			evt.enabled = true;
		}
		//NextSentence (true);
		*/
		yield return null;
	}


	public void AssetAssigner (string CurrentLevel, int lastPage)
	{
		EnvironmentTracker = CurrentLevel;
		MountainTest = GameObject.FindGameObjectWithTag ("MountainRange");
		CharacterCoin = GameObject.FindGameObjectWithTag ("CharacterCoin");
		Characters = GameObject.FindGameObjectsWithTag ("Characters");
		StoryManager = GameObject.FindGameObjectWithTag ("StoryManager");
		DynamicProps = GameObject.FindGameObjectsWithTag ("DynamicProps");
		//Wait a frame for all scenes to be loaded
		//Camera tracking variables

		foreach (GameObject obj in Characters) {
			if (obj.GetComponent<Camera> () != null) {
				cameraTransformTracker = obj.transform;
			}
		}
		cameraPreviousPosition = cameraTransformTracker.position;
		transform.hasChanged = false;
		LastPageLoader = lastPage;
		if (isGoingBack == true) {
			sceneindex = LastPageLoader;
			SetToLastPosition ();
		}
	}

	public void ChapterskipSetCharacters (int StartingPosition)
	{
		foreach (GameObject Child in Characters) {//Play the next animation on all the characters
			if (Child.GetComponent<Animator> () != null || Child.GetComponent<Camera> () != null || Child.GetComponent<Image> () != null) {
				Child.GetComponent<CharacterAnimationSystems> ().setUpCharacters (StartingPosition);									
			}
		}
	}

	public void ChapterSkipAddOffset (int offset)
	{
		ChapterOffSet = offset;
	}

	public void ChapterSkip (String LevelToLoad)
	{
		SceneManager.LoadScene (LevelToLoad, LoadSceneMode.Additive);
		SceneManager.UnloadScene (EnvironmentTracker);
		isGoingBack = false;
		sceneindex = 0;
	}

	public void ChapterSkipToTheEnd (String LevelToLoad)
	{
		SceneManager.LoadScene (LevelToLoad, LoadSceneMode.Additive);
		SceneManager.UnloadScene (EnvironmentTracker);
		//StoryManager.GetComponent<StoryManager> ().StartFromEndOfLevel ();
		//isGoingBack = false;
		//sceneindex = 0;
	}


	void Update ()
	{
		if (cameraTransformTracker != null && CanSwipe == true) {
			

			if (Input.GetMouseButtonDown (0)) {//If the player is holding down the mouse. 
				isMouseMoving = true;
				mouseStartPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
				Debug.Log(mouseStartPosition);
			} else if (Input.GetMouseButtonUp (0)) {
				mouseEndPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);	
				isMouseMoving = false;	
				SetChanger ();
			} 

			/*if (Input.GetMouseButtonDown (0)) 
				{
				mouseoffset = Vector2.Distance( 
					new Vector2(Input.mousePosition.x, TextBody.transform.position.y),
					new Vector2(TextBody.transform.position.x, TextBody.transform.position.y)
				);
				}
			else if (Input.GetMouseButton (0)) {
				TextBody.transform.position = new Vector2 (Input.mousePosition.x+mouseoffset, TextBody.transform.position.y);
			}
			/*if (cameraTransformTracker != null) {
				if (cameraTransformTracker.hasChanged) {
					//print("The transform has changed!");
					cameraTransformTracker.hasChanged = false;
				}			
			}*/
		}
	}


	private void SetChanger ()
	{

		if ((mouseStartPosition.x - mouseEndPosition.x) > 300 || (mouseStartPosition.x - mouseEndPosition.x) < -300) {
			if (mouseStartPosition.x > mouseEndPosition.x && !EventSystem.current.IsPointerOverGameObject ()) {//If the player swipes to the next page
				sceneindex++;
				//Debug.Log (sceneindex);
				//sceneindex
				if (sceneindex >= StoryManager.GetComponent<StoryManager> ().pagesPerScene) {
					GameObject Canvas = GameObject.FindGameObjectWithTag ("Canvas");
					//Check if the player has reached the end of this scene, Once reached, go to the next scene.
					SceneManager.LoadScene (StoryManager.GetComponent<StoryManager> ().NextScene, LoadSceneMode.Additive);
					//EnvironmentTracker
					if (StoryManager.GetComponent<StoryManager> ().isLastscene == true) {				
						Canvas.GetComponent<MainStoryScreen> ().OnQuitButton ();
					} else {
						SceneManager.UnloadScene (EnvironmentTracker);
					}

					isGoingBack = false;
					sceneindex = 0;
				} else {
					//Debug.Log (sceneindex);
					NextSentence (isForward);
					isGoingBack = false;
					isForward = true;
					transform.hasChanged = false;

					foreach (GameObject Mesh in DynamicProps) 
					{
						Mesh.GetComponent<DynamicStaticMeshSystem> ().MoveMeshForward ();
					}

					foreach (GameObject Child in Characters) 
					{//Play the next animation on all the characters
						if (Child.GetComponent<Animator> () != null || Child.GetComponent<Camera> () != null || Child.GetComponent<Image> () != null) {
							Child.GetComponent<CharacterAnimationSystems> ().InvokeNextAnimation ();									
						}
					}
					UIDots.GetComponent<DotGenerator> ().updateDots (sceneindex);

				}

			} else {
				sceneindex--;
				if (sceneindex <= 0) {

					if (StoryManager.GetComponent<StoryManager> ().isFirstscene == true) {

						GameObject Canvas = GameObject.FindGameObjectWithTag ("Canvas");
						Canvas.GetComponent<MainStoryScreen> ().OnQuitButton ();
					} else {
						isGoingBack = true;
						ChapterSkipToTheEnd (StoryManager.GetComponent<StoryManager> ().LastScene);
					}
				} else {
					PreviousSentence (isForward);
					isForward = false;
					transform.hasChanged = false;

					foreach (GameObject Mesh in DynamicProps) 
					{
						Mesh.GetComponent<DynamicStaticMeshSystem> ().MoveMeshBackward ();
					}

					//Play the current animation
					foreach (GameObject Child in Characters) {
						if (Child.GetComponent<Animator> () != null || Child.GetComponent<Camera> () != null || Child.GetComponent<Image> () != null) {
							//Debug.Log("Launching Previous Anim");
							Child.GetComponent<CharacterAnimationSystems> ().InvokePreviousAnimation ();
						}
					}
					UIDots.GetComponent<DotGenerator> ().updateDots (sceneindex);
				}
			}
			//Debug.Log (sceneindex);
		}
		CharacterCoin.GetComponent<SpeakerUIAssign> ().ImageAssign (Speaker);	
	}


	private void SetToLastPosition ()
	{
		
	Characters = GameObject.FindGameObjectsWithTag ("Characters");
		foreach (GameObject Child in Characters) {

			Child.GetComponent<CharacterAnimationSystems> ().ResetToTheEnd ();

		}

	DynamicProps = GameObject.FindGameObjectsWithTag ("DynamicProps");
		foreach (GameObject Mesh in DynamicProps) 
		{
			Mesh.GetComponent<DynamicStaticMeshSystem> ().ResetToTheEnd ();
		}

	}

	private void LanguageMenuDeploy ()
	{
		GameObject Canvas = GameObject.FindGameObjectWithTag ("Canvas");
		GameObject text = GameObject.FindGameObjectWithTag ("DebugText");
		//Canvas Positions
		Vector3 OGPosition = text.transform.position;
		Vector3 Position = text.transform.position;
		//GoToPage(2);
		//currentPage.audioObjects.Count + "///" + currentStory.pageObjects.Count;
		for (int languageCount = 0; languageCount < DataManager.languageManager.Length; languageCount++) {
			GameObject LanguageButton = Instantiate (text) as GameObject;
			//Attributes
			LanguageButton.GetComponent<Button> ().GetComponentInChildren<Text> ().text = DataManager.languageManager [languageCount];
			LanguageButton.GetComponent<Button> ().GetComponent<Image> ().color = Color.blue;
			LanguageButton.transform.SetParent (Canvas.transform, false);
			Position = new Vector3 (Position.x + 100, Position.y, Position.z);
			LanguageButton.transform.position = Position;
			LanguageButton.GetComponent<Button> ().onClick.AddListener (() => OnUIButtonClick_Language (LanguageButton.GetComponent<Button> ()));
		}
	}

	private void OnUIButtonClick_Language (Button button)
	{//when the player clicks a button
		//Debug.Log("OnUIButtonClick_Language: " + button.gameObject.GetComponentInChildren<Text>().text);
		ChangeLanguage (button.gameObject.GetComponentInChildren<Text> ().text);
	}

	private void MenuSetup ()//OBSELETE 
	{///
		//Debug.Log(currentStory.pageObjects.Count);
		GameObject Canvas = GameObject.FindGameObjectWithTag ("Canvas");
		GameObject text = GameObject.FindGameObjectWithTag ("DebugText");
		//Canvas Positions
		Vector3 OGPosition = text.transform.position;
		Vector3 Position = text.transform.position;
		//GoToPage(2);
		//currentPage.audioObjects.Count + "///" + currentStory.pageObjects.Count;
		for (int pageCount = 0; pageCount < currentStory.pageObjects.Count; pageCount++) {//Cycle through all the pages.
			GameObject ChapterButton = Instantiate (text) as GameObject;
			ChapterButton.transform.SetParent (Canvas.transform, false);
			//Positions
			Position = OGPosition;
			ChapterButton.transform.position = Position;
			//Button Edit
			//Button button;
			//button = ChapterButton.GetComponent<Button>();
			//Attributes 
			ChapterButton.GetComponent<Image> ().color = Color.red;
			ChapterButton.GetComponentInChildren<Text> ().text = "Chapter:" + (pageCount + 1);
			//Debug.Log("page" + pageCount);
			for (int audio = 0; audio < currentStory.pageObjects [pageCount].audioObjects.Count; audio++) {//Cycle through all the audio clips. 
				GameObject AudioPoint = Instantiate (text) as GameObject;
				AudioPoint.transform.SetParent (Canvas.transform, false);
				Position = new Vector3 (Position.x + 100, Position.y, Position.z);
				AudioPoint.transform.position = Position;
				//button = AudioPoint.GetComponent<Button>();
				//Attributes 
				AudioPoint.GetComponent<Button> ().onClick.AddListener (() => OnUIButtonClick_Menu (AudioPoint.GetComponent<Button> ()));
				AudioPoint.GetComponent<Button> ().GetComponent<Image> ().color = Color.green;
				AudioPoint.GetComponent<Button> ().GetComponentInChildren<Text> ().text = audio.ToString ();
			}
			OGPosition = new Vector3 (OGPosition.x, OGPosition.y - 30.0f, OGPosition.z);
		}
		text.gameObject.SetActive (false);
	}

	private void OnUIButtonClick_Menu (Button button)
	{//when the player clicks a button
		Debug.Log ("OnUIButtonClick_Menu: " + button.gameObject.GetComponentInChildren<Text> ().text);
		int Page = int.Parse (button.gameObject.GetComponentInChildren<Text> ().text);
		//GoToPage (Page);
	}

	void TaskOnClick ()
	{
		Debug.Log ("You have clicked the button!");
	}

	public void ChangeLanguage (string newLanguage)
	{
		StopAllCoroutines ();
		sentenceContainer.Clear ();
		//Debug.Log(newLanguage);

		DataManager.currentLanguage = newLanguage;
		DataManager.LoadStory (DataManager.currentStoryName);
		PreviousSentence (true);
	}

	public void Register (TweenEvent evt)
	{
		tweenEvents.Add (evt);
		evt.id = tweenEvents.Count.ToString ();
	}

	public void KillCurrentCoroutines ()
	{
		//isMenuDeployed = true;
		StopAllCoroutines ();
		sentenceContainer.Clear ();
	}

	public void ReactivateCurrentCoroutine ()
	{
		//isMenuDeployed = false;
		StopAllCoroutines ();
		sentenceContainer.Clear ();
		StartCoroutine (RunSequence (currentPage.audioObjects [audioIndex]));
	}

	public void GoToPage (int i)
	{
		//Debug.Log(isGoingBack);

		StopAllCoroutines ();
		sentenceContainer.Clear ();
		audioIndex = i;
		StartCoroutine (RunSequence (currentPage.audioObjects [audioIndex]));
		if (isGoingBack == false) {
		}

	}

	void PreviousSentence (bool playFromLast)
	{//Turn off the current passage and prep for the next passage
		StopAllCoroutines ();
		sentenceContainer.Clear ();

		if (audioIndex < 1 && pageIndex > 0) {//Switch to the previous page if can(OBSELETE)
			Debug.Log ("Reset to previous page");
			pageIndex--;
			audioIndex = currentStory.pageObjects.Count + 1;
		}

		AudioObject currentAudio = currentPage.audioObjects [audioIndex];
		foreach (TweenEvent evt in tweenEvents) {
			if (currentPage.name == evt.pageName && currentAudio.name == evt.audioName) {
				evt.OnDeactivate ();
			}
		}

		PlayPreviousSentence ();

		if (playFromLast == true) {//If the player flips to a previous page after moving forward previously
			PreviousSentence (false);
		}
	}

	void PlayPreviousSentence ()
	{

		if (audioIndex > 0) {//reduce the passage book mark
			audioIndex--;
		}

		AudioObject currentAudio = currentPage.audioObjects [audioIndex];
		//Actiavte tweens
		foreach (TweenEvent evt in tweenEvents) {
			evt.OnNextStep ();
			if (currentPage.name == evt.pageName && currentAudio.name == evt.audioName) {
				evt.OnActivate ();
			}
		}

		StartCoroutine (RunSequence (currentAudio));
		//Debug.Log(audioIndex + "/" + pageIndex);
	}


	void NextSentence (bool playFromLast)
	{//Move the narrative forward
		//Debug.Log("working");
		StopAllCoroutines ();
		sentenceContainer.Clear ();
		if (pageIndex >= currentStory.pageObjects.Count) {//when the player reaches the end of the narrative
			Debug.Log ("Story ended! Back to menu...");
			//SceneManager.LoadScene("Menu");
			return;
		}

		AudioObject currentAudio = currentPage.audioObjects [audioIndex];
		foreach (TweenEvent evt in tweenEvents) {
			if (currentPage.name == evt.pageName && currentAudio.name == evt.audioName) {
				evt.OnDeactivate ();              
			}
		}

		PlayCurrentSentence ();
		if (playFromLast == false) {
			NextSentence (true);
		}

	}


	void PlayCurrentSentence ()
	{
		audioIndex++;
		AudioObject currentAudio = currentPage.audioObjects [audioIndex];

		//Scenetext.GetComponent<Text> ().text = DataManager.SceneRef [audioIndex];

		//Actiavte tweens
		foreach (TweenEvent evt in tweenEvents) {
			evt.OnNextStep ();
			if (currentPage.name == evt.pageName && currentAudio.name == evt.audioName) {
				evt.OnActivate ();               
			}
		}
		StartCoroutine (RunSequence (currentAudio));
		//Debug.Log(audioIndex + "/" + pageIndex);
	}

	IEnumerator RunSequence (AudioObject obj)
	{
		//Debug.Log ("sequence running");
		if (obj.clip != null) {
			audioSource.clip = obj.clip;
			audioSource.Play ();
		} else {
			Debug.LogErrorFormat ("Unable to read the audio from folder {0}. " +
			"Please ensure an audio file is in the folder, and it's set to the assetbundle {1}.", obj.name, DataManager.currentStoryName);
		}

		if (obj.sentence == null) {
			Debug.LogErrorFormat ("Unable to read the text from folder {0}. " +
			"Please ensure a text file is in the folder, and it's  set to the assetbundle {1}.", obj.name, DataManager.currentStoryName);
			yield break;
		}
		//Displaying all words in the bottom
		foreach (WordGroupObject wordGroup in obj.sentence.wordGroups) {
			if (wordGroup.text.Contains ("speaker")) {//Get The Narrator
				Speaker = wordGroup.text;
				Speaker = Speaker.Remove (0, 10);
				//Debug.Log(Speaker);
			} else {				
				//Debug.Log (wordGroup.text);
				sentenceContainer.AddText (wordGroup);
			}
		}
		//highlight the proper wordgroups
		int i = 0;
		WordGroupObject prevWordGroup = null;

		while (i < obj.sentence.wordGroups.Count) {
			WordGroupObject wordGroup = obj.sentence.wordGroups [i];
			sentenceContainer.HighlightWordGroup (wordGroup);
			i++;
			//We calculate it like this because the times given are actually absolute times, not times per word
			float waitTime = wordGroup.time;
			if (prevWordGroup != null) {
				waitTime -= prevWordGroup.time;
			}
			yield return new WaitForSeconds (waitTime);
			prevWordGroup = wordGroup;
		}
		sentenceContainer.HighlightWordGroup (null);
	}

	public static List<T> FindObjectsOfTypeAll<T> ()
	{
		List<T> results = new List<T> ();
		for (int i = 0; i < SceneManager.sceneCount; i++) {
			var s = SceneManager.GetSceneAt (i);
			if (s.isLoaded) {
				var allGameObjects = s.GetRootGameObjects ();
				for (int j = 0; j < allGameObjects.Length; j++) {
					var go = allGameObjects [j];
					results.AddRange (go.GetComponentsInChildren<T> (true));
				}
			}
		}
		return results;
	}

	public void ChangeMusic (Slider newMusicVolume)
	{
		Debug.Log ("newMusicVolume" + newMusicVolume.value);
		audioSource.volume = newMusicVolume.value;
	}

	public void ChangeNarrative (Slider newNarrativeVolume)
	{
		Debug.Log ("newNarrativeVolume" + newNarrativeVolume.value);
		audioSource.volume = newNarrativeVolume.value;
	}

	public void ChangeTextStyle (Slider newTextStyle)
	{
		Debug.Log ("newTextStyle" + newTextStyle.value);
		//audioSource.volume = newTextStyle.value;
		ScentenceContainer.GetComponent<SentenceRowContainer> ().ReadAlongOn = newTextStyle.value;
	}

	public void changeClickDragFunctionality (bool state)
	{
		CanSwipe = state;
	}

}
