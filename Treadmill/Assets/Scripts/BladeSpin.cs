﻿using UnityEngine;
using System.Collections;

public class BladeSpin : MonoBehaviour {

	public float spinSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float currentSpeed = Mathf.LerpAngle (0, spinSpeed, Time.time/6);
		//Debug.Log ("currentSpeed = " + currentSpeed);
		transform.Rotate (new Vector3(0, 0, currentSpeed));
	
	}
}
