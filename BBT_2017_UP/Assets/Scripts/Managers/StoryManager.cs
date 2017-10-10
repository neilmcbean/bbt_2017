using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour {

	public int AudioIndexPosition; 
	public string LevelName;
	public int pagesPerScene;
	public string NextScene;
	public string LastScene;
	private GameObject PageManager;

	// Use this for initialization
	void Start () {
		PageManager = GameObject.FindGameObjectWithTag ("PageManager");
		PageManager.GetComponent<PageManager> ().AssetAssigner (LevelName,pagesPerScene);
		PageManager.GetComponent<PageManager> ().GoToPage (AudioIndexPosition);



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
