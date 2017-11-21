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
	public GameObject PageManager;

	// Use this for initialization
	void Start () {
		PageManager = GameObject.FindGameObjectWithTag ("PageManager");

		if (PageManager.GetComponent<PageManager> ().isGoingBack == false) {
			//Debug.Log ("isGoingBack="+PageManager.GetComponent<PageManager> ().isGoingBack);
			PageManager.GetComponent<PageManager> ().AssetAssigner (LevelName, AudioIndexPosition);
			PageManager.GetComponent<PageManager> ().GoToPage (AudioIndexPosition);	
		} else {
			//Debug.Log ("isGoingBack="+PageManager.GetComponent<PageManager> ().isGoingBack);
			PageManager = GameObject.FindGameObjectWithTag ("PageManager");
			PageManager.GetComponent<PageManager> ().AssetAssigner (LevelName,pagesPerScene);
			PageManager.GetComponent<PageManager> ().GoToPage (AudioIndexPosition+pagesPerScene);	
			PageManager.GetComponent<PageManager> ().isGoingBack = false;
		}
	}

	public void StartFromEndOfLevel () {

	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
