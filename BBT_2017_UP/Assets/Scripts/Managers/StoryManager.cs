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
	private GameObject Canvas;

	// Use this for initialization
	void Start () {
		PageManager = GameObject.FindGameObjectWithTag ("PageManager");
		Canvas = GameObject.FindGameObjectWithTag ("Canvas");

		for(int i = 0; i < Canvas.transform.GetChildCount(); i++)
		{
			if (Canvas.transform.GetChild (i).name == "UI Dots") {
				Canvas.transform.GetChild (i).GetComponent<DotGenerator> ().GenrateTheDots (pagesPerScene);
			}
			//GameObject Go = this.gameobject.transform.GetChild(i);
		}

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
