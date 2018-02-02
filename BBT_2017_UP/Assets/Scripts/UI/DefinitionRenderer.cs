using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefinitionRenderer : MonoBehaviour {

	public Image RenderPlacement;

	public string[] def_wordSas;
	public Sprite [] def_wordSasPhotos;
	public string[] def_BodySasEnglish;


	public string[] def_wordLilPpl;
	public Sprite [] def_wordLilPplPhotos;
	public string[] def_BodyLilPplEnglish;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetUpText (Text Definition) 
	{
		Debug.Log (Definition.text);
		for (int i = 0; i < def_wordSas.Length; i++) 
		{
			//Debug.Log (Definition.text.ToLower()+"///"+def_wordSas[i].ToLower());

			if (Definition.text.ToLower().Equals(def_wordSas[i].ToLower())) {
			//if(String.Compare(Definition.text.ToLower() , def_wordSas[i].ToLower()) == 1){
			//if ( Definition.text.ToLower() == def_wordSas[i].ToLower()) {
				GetComponentInChildren<Text> ().text = def_BodySasEnglish [i];
				RenderPlacement.sprite = def_wordSasPhotos [i];

				Debug.Log (Definition.text.ToLower()+"///"+def_wordSas [i]);
			} 
		}
	}

}
