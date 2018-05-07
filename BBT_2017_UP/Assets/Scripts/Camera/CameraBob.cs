using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour {

	private float timer = 0.0f;
	float bobbingSpeed = 0.02f;
	float bobbingAmount = 0.002f;
	float midpoint_Y = 2.0f;
	float midpoint_X = 2.0f;
	void Update () {
	float waveslice = 0.0f;
	float horizontal = 0.03f;
	float vertical = 0.03f;
	int direction_Changer =1;
	int dieRoll = Random.Range (0, 3);

	Vector3 cSharpConversion = transform.position; 

	float midpoint_Y = transform.position.y; 
	float midpoint_X = transform.position.x;
		if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) 
		{
		timer = 0.0f;
			/*int dieRoll;
			dieRoll = Random.Range (0, 2);
			if (dieRoll == 0) 
			{
				direction_Changer = -1;
			}
				else if (dieRoll == 1) 
				{
					direction_Changer = 0;
				}
					else if (dieRoll == 2) 
					{
						direction_Changer = 1;
					}*/
		}
			else 
			{
			waveslice = Mathf.Sin(timer);
			timer = timer + bobbingSpeed;
				if (timer > Mathf.PI * 2) 
				{
				dieRoll = Random.Range (0, 3);					
				Debug.Log (direction_Changer+"//"+dieRoll);
				timer = timer - (Mathf.PI * 2);
				}
			}
		if (waveslice != 0) 
		{
		float translateChange = waveslice * bobbingAmount;
		float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
		totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f);
		translateChange = totalAxes * translateChange;
		cSharpConversion.y = midpoint_Y + translateChange;
		
			if (dieRoll == 0) 
			{
			cSharpConversion.x = midpoint_X + (translateChange);
			}
				else if (dieRoll == 1) 
				{
				cSharpConversion.x = midpoint_X + (translateChange)*-1;
				}
					else if (dieRoll == 2) 
					{
					//cSharpConversion.x = midpoint_X;
					}		
		}
			else 
			{
			cSharpConversion.y = midpoint_Y;
			//cSharpConversion.x = midpoint_X;


			}
	transform.localPosition = cSharpConversion;
	}
}
