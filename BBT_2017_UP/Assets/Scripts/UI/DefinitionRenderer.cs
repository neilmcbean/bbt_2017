using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefinitionRenderer : MonoBehaviour {

	public string[] def_word;
	public string[] def_Body;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetUpText (string Definition) 
	{
		for (int i = 0; i < def_word.Length; i++) 
		{	
			//Debug.Log (def_word [i]+"///"+Definition);
			if ( Definition == def_word [i]) {
				GetComponentInChildren<Text> ().text = def_Body [i];
			} 
		}
	}

}
