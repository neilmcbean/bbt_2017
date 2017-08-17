using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour {

	public string LevelName;
	public int pagesPerScene;
	public string NextScene;
	private GameObject PageManager;

	// Use this for initialization
	void Start () {
		PageManager = GameObject.FindGameObjectWithTag ("PageManager");
		PageManager.GetComponent<PageManager> ().AssetAssigner (LevelName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
