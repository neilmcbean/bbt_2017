using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class VideoManager : MonoBehaviour {


	public VideoPlayer[] videoClips;
	private bool isSasOpen = false;
	private bool isLiLOpen = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeSasSource()
	{
		foreach (VideoPlayer i in videoClips) {
			
			//i.GetComponent<VideoPlayer> ().enabled = false;
		}

		//videoClips.GetComponent<VideoPlayer> ().setActive
		if (isSasOpen == true) {
			isSasOpen = false;
			videoClips [3].enabled = true;
			videoClips [3].Play ();
			//videoClips.enabled = true;
		} else {
			isSasOpen = true;
			videoClips [4].enabled = true;
			videoClips [4].Play ();
		}
	}

	public void ChangeLilSource()
	{
		if (isLiLOpen == true) {
			isLiLOpen = false;
		} else {
			isLiLOpen = true;
		}
	}
}
