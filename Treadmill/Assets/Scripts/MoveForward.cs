using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

	Rigidbody2D locRig;
	public float stepSize = 1f;
	public static float antForceDiv;
	public bool beginPush = false;
	private bool leftFoot = true; //use a boolean to alternate between the required keystrokes

	// Use this for initialization
	void Start () {
		locRig = GetComponent<Rigidbody2D> ();
		antForceDiv = 6f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		KeyCode whichFoot;
		if (leftFoot){
			whichFoot = KeyCode.S;
		} else {
			whichFoot = KeyCode.D;
		}

		if (Input.GetKeyDown(whichFoot)) {
			locRig.AddForce (Vector2.right * stepSize);
			leftFoot = !leftFoot;
		}

		if (beginPush) {
			locRig.AddForce (Vector2.left * stepSize / antForceDiv);
		}
	}

	void OnCollisionEnter2D(Collision2D myCol){
		if (!beginPush) {
			beginPush = true;
		}
	}
}
