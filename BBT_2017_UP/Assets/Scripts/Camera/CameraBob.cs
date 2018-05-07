using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour {

	private float timer = 0.0f;
	float bobbingSpeed = 0.02f;
	float bobbingAmount = 0.002f;
	float midpoint_Y = 2.0f;
	float midpoint_X = 2.0f;
	float waveslice = 0.0f;
	float horizontal = 0.03f;
	float vertical = 0.03f;
	public int direction_Changer =1;
	int dieRoll = Random.Range (0, 3);
	private float timeLeft = Random.Range (1, 3);
	private bool isZoomed = false;
	private float OG_FOV;
	void Start () {
		OG_FOV = GetComponent<Camera> ().fieldOfView;
		//Shake();
	}


	void Update () {

		if (GetComponent<Camera> ().enabled == true) 
		{		
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				//timeLeft = 0;
				if (isZoomed == false) 
				{
				GetComponent<Camera> ().fieldOfView -= Random.Range (2, 8);
				isZoomed = true;
				timeLeft = Random.Range (1, 2);
				} 
					else 
					{
					GetComponent<Camera> ().fieldOfView = OG_FOV;
					isZoomed = false;
					timeLeft = Random.Range (4, 8);
					}

				//Debug.Log ("Working");
			}


			Vector3 cSharpConversion = transform.position; 

			float midpoint_Y = transform.position.y; 
			float midpoint_X = transform.position.x;
			if (Mathf.Abs (horizontal) == 0 && Mathf.Abs (vertical) == 0) {
				timer = 0.0f;
				//Debug.Log (direction_Changer+"//"+dieRoll);
			} else {
				waveslice = Mathf.Sin (timer);
				timer = timer + bobbingSpeed;
				if (timer > Mathf.PI * 2) {
					dieRoll = Random.Range (0, 3);	
					if (dieRoll == 0) {
						direction_Changer = -1;
					} else if (dieRoll == 1) {
						direction_Changer = 1;
					} else if (dieRoll == 2 || dieRoll == 3) {
						direction_Changer = 0;
					}

					timer = timer - (Mathf.PI * 2);
				}
			}
			if (waveslice != 0) {
				float translateChange = waveslice * bobbingAmount;
				float totalAxes = Mathf.Abs (horizontal) + Mathf.Abs (vertical);
				totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f);
				translateChange = totalAxes * translateChange;
				cSharpConversion.y = midpoint_Y + translateChange;
				cSharpConversion.x = midpoint_X + (translateChange / 2) * direction_Changer;
			} else {
				cSharpConversion.y = midpoint_Y;
				cSharpConversion.x = midpoint_X;

			}
			transform.localPosition = cSharpConversion;
		}
	}
}
