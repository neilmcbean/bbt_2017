using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationSystems : MonoBehaviour {

	private int AnimationDelayTracker;
	public int AnimationDelay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InvokeNextAnimation()
	{
		if (AnimationDelayTracker >= AnimationDelay) {
			GetComponent<Animator> ().SetTrigger ("advance");
			Debug.Log (gameObject.name);
		} else {
			AnimationDelayTracker++;
		}


	}
}
