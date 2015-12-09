using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

	Rigidbody2D locRig;
	public float stepSize = 1f;

	// Use this for initialization
	void Start () {
		locRig = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.D)) {
			locRig.AddForce (Vector2.right * stepSize);
		}


		locRig.AddForce (Vector2.left * stepSize/4);

	}
}
