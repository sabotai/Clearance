using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

	Rigidbody2D locRig;
	public float stepSize = 1f;
	public static float antForceDiv;
	public static bool beginPush = false;
	private bool leftFoot = true; //use a boolean to alternate between the required keystrokes
	public AudioSource audioContainer;
	public AudioClip leftStep;
	public AudioClip rightStep;

	// Use this for initialization
	void Start () {
		locRig = GetComponent<Rigidbody2D> ();
		antForceDiv = 6f;
		beginPush = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	if (beginPush) {
		KeyCode whichFoot;
		if (leftFoot){
			whichFoot = KeyCode.S;
		} else {
			whichFoot = KeyCode.D;
		}

		if (Input.GetKeyDown(whichFoot)) {
			locRig.AddForce (Vector2.right * stepSize);
			leftFoot = !leftFoot;

			audioContainer.Stop ();
			if (leftFoot) {
				audioContainer.PlayOneShot (leftStep, 0.2f);
			} else {
				
				audioContainer.PlayOneShot (rightStep, 0.2f);
			}

		}

			locRig.AddForce (Vector2.left * stepSize / antForceDiv);
		}
	}

	void OnCollisionEnter2D(Collision2D myCol){
		if (!beginPush) {
			beginPush = true;
		}
	}

	void OnTriggerEnter2D(Collider2D myTrig){
		Debug.Log ("triggered");
		if (myTrig.gameObject.tag == "item") {
			CurrencyGen.score++;
			Destroy (myTrig.gameObject);
		}
	}
}
