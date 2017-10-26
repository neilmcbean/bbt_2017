using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterAnimationSystems : MonoBehaviour
{

	public int AnimationDelayTracker = 0;
	//public int AnimationDelay;
	public bool[] AnimationBool;
	public string LastAnimation;
	// Use this for initialization

	void Start () {
		if (AnimationBool [AnimationDelayTracker] == true) 
		{			
			if (GetComponent<Camera> () != null) 
			{//if the thing to check has a camera.
				//Debug.Log(gameObject.name+"turn on");
				//gameObject.SetActive (true);
				gameObject.GetComponent<Camera>().enabled = true;
			} 
			else 
			{
				if (GetComponent<Image> () != null) 
				{
				GetComponent<Image> ().enabled = true;
				} 
				else
				{//if(GetComponent.
					GetComponent<Animator> ().enabled = true;
					foreach (Transform child in transform) 
					{
						if (child.gameObject.GetComponent<SkinnedMeshRenderer> () != null)
						child.gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = true;
					}
				}
			}

		} else {

					if (GetComponent<Camera> () != null) 
					{//if the thing to check has a camera.
						//Debug.Log(gameObject.name+"turn off");
						//gameObject.SetActive (false);
						gameObject.GetComponent<Camera>().enabled = false;
					} 
						else 
						{
							if (GetComponent<Image> () != null) 
							{
								GetComponent<Image> ().enabled = false;
							} 
								else 
								{
									//if(GetComponent.
									GetComponent<Animator> ().enabled = false;
									foreach (Transform child in transform) 
									{
										if (child.gameObject.GetComponent<SkinnedMeshRenderer> () != null)
											child.gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = false;
									}
								}
						}
				}
	}

	public void InvokeNextAnimation ()
	{	
		if (AnimationDelayTracker <AnimationBool.Length-1) {
			AnimationDelayTracker++;	
			if (AnimationBool [AnimationDelayTracker] == true) {			

				if (GetComponent<Camera> () != null) {//if the thing to check has a camera.
					//gameObject.SetActive (true);
					gameObject.GetComponent<Camera> ().enabled = true;
					if (GetComponent<Animator> () != null) {
						GetComponent<Animator> ().SetTrigger ("advance");
					}
				} 

				else 
				{//Turn on the renderer

					if (GetComponent<Image> () != null) {
						GetComponent<Image> ().enabled = true;
					} else {

						GetComponent<Animator> ().enabled = true;
						foreach (Transform child in transform) {
							if (child.gameObject.GetComponent<SkinnedMeshRenderer> () != null)
								child.gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = true;
						}
						//Debug.Log(gameObject.name+"runing anim");
						GetComponent<Animator> ().SetTrigger ("advance");
					}
				}

			} else {

				if (GetComponent<Camera> () != null) {//if the thing to check has a camera.
					//Debug.Log(gameObject.name+"turn off");
					//gameObject.SetActive (false);
					gameObject.GetComponent<Camera> ().enabled = false;
				} else {//Turn off the renderer

					if (GetComponent<Image> () != null) 
					{
					GetComponent<Image> ().enabled = false;
					} 
					else 
					{
						gameObject.GetComponent<Animator> ().enabled = false;
							foreach (Transform child in transform) 
						{
							if (child.gameObject.GetComponent<SkinnedMeshRenderer> () != null)
								child.gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = false;
						}
					}
				}
			}
		}
		//Debug.Log(gameObject.name);
	}

	public void InvokePreviousAnimation ()
	{
		if (AnimationDelayTracker > 0) {
			AnimationDelayTracker--;
			if (AnimationBool [AnimationDelayTracker] == true) {			

				if (GetComponent<Camera> () != null) 
				{//if the thing to check has a camera.
					//gameObject.SetActive (true);
					gameObject.GetComponent<Camera>().enabled = true;
					if(GetComponent<Animator>() != null)
					{
						GetComponent<Animator>().SetTrigger("goback");
					}
				} 
				else 
				{//Turn on the renderer

					if (GetComponent<Image> () != null) {
						GetComponent<Image> ().enabled = true;
					} else {

						GetComponent<Animator> ().enabled = true;
						foreach (Transform child in transform) {
							if (child.gameObject.GetComponent<SkinnedMeshRenderer> () != null)
								child.gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = true;
						}

						GetComponent<Animator> ().SetTrigger ("goback");
					}
				}
			} else {

				if (GetComponent<Camera> () != null) 
				{//if the thing to check has a camera.
					//Debug.Log(gameObject.name+"turn off");
					//gameObject.SetActive (false);
					gameObject.GetComponent<Camera>().enabled = false;
				} 
				else 
				{//Turn on the renderer

					if (GetComponent<Image> () != null) {
						GetComponent<Image> ().enabled = false;
					} else {

						gameObject.GetComponent<Animator> ().enabled = false;
						foreach (Transform child in transform) {
							if (child.gameObject.GetComponent<SkinnedMeshRenderer> () != null)
								child.gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = false;
						}
					}
				}
				}

		}
	}

	public void ResetToTheEnd ()
	{
		AnimationDelayTracker = AnimationBool.Length - 1;
		Debug.Log(gameObject.name+"turn on");	
		if (AnimationBool [AnimationDelayTracker] == true) 
		{		
			
			if (GetComponent<Camera> () != null) 
			{//if the thing to check has a camera.
				
				//gameObject.SetActive (true);
				gameObject.GetComponent<Camera>().enabled = true;
				if(GetComponent<Animator>() != null)
				{
					GetComponent<Animator>().SetTrigger(LastAnimation);
				}
			} 
			else 
			{
				if (GetComponent<Image> () != null) {
					GetComponent<Image> ().enabled = true;
				} else {
					GetComponent<Animator> ().enabled = true;
					foreach (Transform child in transform) {
						if (child.gameObject.GetComponent<SkinnedMeshRenderer> () != null)
							child.gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = true;
					}
					GetComponent<Animator> ().Play (LastAnimation);
				}
			}

		} else {

			if (GetComponent<Camera> () != null) 
			{//if the thing to check has a camera.
				//Debug.Log(gameObject.name+"turn off");
				//gameObject.SetActive (false);
				gameObject.GetComponent<Camera>().enabled = false;

			} 
			else 
			{//Turn on the renderer

				if (GetComponent<Image> () != null) {
					GetComponent<Image> ().enabled = true;
				} else {

					GetComponent<Animator> ().enabled = false;
					foreach (Transform child in transform) {
						if (child.gameObject.GetComponent<SkinnedMeshRenderer> () != null)
							child.gameObject.GetComponent<SkinnedMeshRenderer> ().enabled = false;
					}
				}
			}
		}

		//GetComponent<Animator> ().Play (LastAnimation);
	}

	private void CharacterTurnOff()
	{
		
	}
}
