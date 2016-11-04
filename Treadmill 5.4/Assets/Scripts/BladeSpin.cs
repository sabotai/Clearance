using UnityEngine;
using System.Collections;

public class BladeSpin : MonoBehaviour {

	public float spinSpeed = 1f;
	public bool steadySpin = false;
	//public bool rot

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float currentSpeed; 
		if (steadySpin) {
			currentSpeed = spinSpeed;
		} else {
			currentSpeed = Mathf.LerpAngle (0, spinSpeed, Time.time / 6);
		}
		//Debug.Log ("currentSpeed = " + currentSpeed);
		transform.Rotate (new Vector3(0, 0, currentSpeed));
	
	}
}
