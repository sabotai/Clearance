using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

	Rigidbody2D locRig;
	public float stepSize = 1f;
	public static float antForceDiv;
	public static bool beginPush = false;
	private bool leftFoot = true; //use a boolean to alternate between the required keystrokes
	public bool easyMode = false;
	public AudioSource audioContainer, audioContainer2;
	public AudioClip leftStep;
	public AudioClip rightStep;
	public AudioClip getItem;

	int stepCountAnim =0;
	public int stepAnimThresh = 15;

	public Transform blade;

	Animator charAnim;

	// Use this for initialization
	void Start () {
		locRig = GetComponent<Rigidbody2D> ();
		antForceDiv = 6f;
		beginPush = false;

		if (easyMode) {
			antForceDiv *= 2;
		}

		charAnim = GetComponent<Animator> ();

		stepCountAnim = stepAnimThresh;
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

			if (Input.GetKeyDown (whichFoot)) {
				//charAnim.SetInteger ("state", 1);
				stepCountAnim = 0;

				locRig.AddForce (Vector2.right * stepSize);
				leftFoot = !leftFoot;

				audioContainer.Stop ();
				if (leftFoot) {
					audioContainer.PlayOneShot (leftStep, 0.8f);
				} else {
				
					audioContainer.PlayOneShot (rightStep, 0.8f);
				}

			} else {
				stepCountAnim++;
			}

			locRig.AddForce (Vector2.left * stepSize / antForceDiv);

			if (charAnim.GetInteger("state") != 9) {
				if ((stepCountAnim > stepAnimThresh) && (Vector3.Distance (transform.position, blade.position) > 4.5)) { //make the figure run when near the blade

					charAnim.SetInteger ("state", 0);

				} else {
					charAnim.SetInteger ("state", 1);
				}
			}

		}
	}

	void OnCollisionEnter2D(Collision2D myCol){
		if (!beginPush) {
			beginPush = true;
			Debug.Log ("FIRST COLLISION");
			charAnim.SetInteger ("state", 0);
		}
	}

	void OnTriggerStay2D(Collider2D myTrig){
		Debug.Log ("triggered");
		if (myTrig.gameObject.tag == "item") {
			audioContainer2.PlayOneShot (getItem, 1f);
			CurrencyGen.score++;
			Destroy (myTrig.gameObject);
		}
	}
}
