using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class VideoManager : MonoBehaviour {

	public GameObject Image;
	public VideoClip[] videoClips;
	public VideoPlayer vidRef;
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

		if (isSasOpen == true) {
			isSasOpen = false;
			GetComponent<VideoPlayer>().clip = videoClips [3];
			GetComponent<VideoPlayer> ().Play ();
		} else {
			isSasOpen = true;
			GetComponent<VideoPlayer>().clip = videoClips [4];
			GetComponent<VideoPlayer> ().Play ();
		}
		Image.SetActive (false);
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
