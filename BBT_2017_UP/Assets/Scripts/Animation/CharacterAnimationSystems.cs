using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationSystems : MonoBehaviour {

	private int AnimationDelayTracker=0;
	public int AnimationDelay;
	public bool[] AnimationBool;
	public string LastAnimation;
	// Use this for initialization
	public void InvokeNextAnimation()
	{	
		AnimationDelayTracker++;	
		if (AnimationBool[AnimationDelayTracker] == true) 
		{			
		foreach(Transform child in transform){child.gameObject.SetActive(true);}
		GetComponent<Animator> ().SetTrigger ("advance");
		//Debug.Log (gameObject.name);
		} else {
		foreach(Transform child in transform){child.gameObject.SetActive(false);}
		}

	}

	public void InvokePreviousAnimation()
	{
		if (AnimationDelayTracker > 0) 
		{
			AnimationDelayTracker--;
			if (AnimationBool [AnimationDelayTracker] == true) 
			{			
				foreach (Transform child in transform) 
				{child.gameObject.SetActive (true);}
				GetComponent<Animator> ().SetTrigger ("goback");
				//Debug.Log ("turningon");
			} else 
			{
			foreach (Transform child in transform) {child.gameObject.SetActive (false);}
			}

		}
	}

	public void ResetToTheEnd()
	{
		AnimationDelayTracker=AnimationBool.Length-1;

		if (AnimationBool [AnimationDelayTracker] == true) 
		{			
		foreach (Transform child in transform) {child.gameObject.SetActive (true);}
		}
		else 
		{foreach (Transform child in transform) {child.gameObject.SetActive (false);}}

		GetComponent<Animator> ().Play (LastAnimation);
	}
}
