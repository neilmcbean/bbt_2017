using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour {
	public float Strength;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(Mathf.PingPong(Time.time/120, Strength), transform.position.y, transform.position.z);
	}
}
