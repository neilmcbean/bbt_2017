using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class VideoManager : MonoBehaviour {


	public VideoClip[] videoClips;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeSource()
	{
		
		//GetComponent<VideoPlayer>().Source = videoClips[3];
	}
}
