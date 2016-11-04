using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

	public Animation runAnim;

	int stepCountAnim =0;
	public int stepAnimThresh = 15;

	public Transform blade;

	Animator charAnim;
	public float animSpeedMultiplier = 1.5f;

	GameObject highScore, highScore2, hs, hs2;

	public Material controlMat, controlMat2;

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

		controlMat.color = new Color (1, 1, 1, 0);
		controlMat2.color = new Color (1, 1, 1, 0);


		highScore = GameObject.Find ("Text-hs");
		highScore2 = GameObject.Find ("Text-hs2");


		hs = GameObject.Find ("Text-hs (1)");
		hs2 = GameObject.Find ("Text-hs2 (1)");

		if (Reset.resetCount > 0) {
			GameObject.Find ("credit").GetComponent<Text> ().color = new Color (0, 0, 0, 0);
			GameObject.Find ("credit2").GetComponent<Text> ().color = new Color (0, 0, 0, 0);

			highScore.GetComponent<Text> ().color = controlMat2.color;
			highScore2.GetComponent<Text> ().color = controlMat2.color;
			hs.GetComponent<Text> ().color = new Color (hs.GetComponent<Text> ().color.r, hs.GetComponent<Text> ().color.g, hs.GetComponent<Text> ().color.b, controlMat2.color.a);
			;
			hs2.GetComponent<Text> ().color = new Color (hs2.GetComponent<Text> ().color.r, hs2.GetComponent<Text> ().color.g, hs2.GetComponent<Text> ().color.b, controlMat2.color.a);
		} else {
			//swap out the high score object on the first run to show the creator credit
			highScore = GameObject.Find ("credit");
			hs = GameObject.Find ("credit2");
		}
		Debug.Log ("high score = " + PlayerPrefs.GetInt ("realHighScore"));
	}


	void FixedUpdate(){
		if (beginPush){
			locRig.AddForce (Vector2.left * stepSize / antForceDiv);



			// animate the running to the same speed as the positive x velocity
			float calcVel = 1 + (locRig.velocity.x-0)*(1.5f - 1)/(4-1); //map range
			//Debug.Log("calculated velocity is ... " + calcVel + "  animSpeed = " + charAnim.speed);

			if (calcVel > 1) {
				charAnim.speed = (calcVel * animSpeedMultiplier);
			} else {
				charAnim.speed = 1;
			}

			/*
			if (charAnim.GetInteger ("state") != 9) { //if not falling
				if ((stepCountAnim > stepAnimThresh) && (Vector3.Distance (transform.position, blade.position) > 4.5f)) { //make the figure run when near the blade

					charAnim.SetInteger ("state", 0);

					charAnim.speed =1;
					//Debug.Log ("anim speed = RESET");


				} else {
					//run while being hit by the blade
					charAnim.SetInteger ("state", 1);
				}
			}
			*/

		}

	}


	// Update is called once per frame
	void Update () {


		float tempPct = Mathf.Clamp (Mathf.Sin (Time.timeSinceLevelLoad), -0.5f, 0) * -2;
		if (tempPct > 0.5f){
			if (controlMat2.color.a > 0){
				controlMat2.color -= new Color (0, 0, 0, .03f);


				highScore.GetComponent<Text> ().color = controlMat2.color;
				hs.GetComponent<Text> ().color = new Color(hs.GetComponent<Text>().color.r, hs.GetComponent<Text>().color.g, hs.GetComponent<Text>().color.b, controlMat2.color.a);

				if (Reset.resetCount > 0) {
					highScore2.GetComponent<Text> ().color = controlMat2.color;	
					hs2.GetComponent<Text> ().color = new Color(hs2.GetComponent<Text>().color.r, hs2.GetComponent<Text>().color.g, hs2.GetComponent<Text>().color.b, controlMat2.color.a);
				}

			}
		}

		if (beginPush) {
			if (controlMat.color.a > 0) {
				controlMat.color -= new Color (0, 0, 0, .05f);
			}

			KeyCode whichFoot;
			if (leftFoot) {
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
					audioContainer.pitch = 0.8f;
					audioContainer.PlayOneShot (leftStep, 0.8f);
				} else {

					audioContainer.pitch = 2f;
					audioContainer.PlayOneShot (rightStep, 0.8f);
				}

			} else {
				//int temp = (int)(60/(1/Time.deltaTime));
				//stepCountAnim += 1 * temp;
				//Debug.Log("temp = " + temp);
				stepCountAnim++;
			}

			//locRig.AddForce (Vector2.left * stepSize / antForceDiv);

			/*
			// animate the running to the same speed as the positive x velocity
			float calcVel = 1 + (locRig.velocity.x-0)*(1.5f - 1)/(4-1); //map range

			if (calcVel > 1) {
				charAnim.speed = calcVel * animSpeedMultiplier * (60/(1/Time.deltaTime));

				Debug.Log ("rigidbody vel = " + charAnim.speed + "   time.dt = " +  (60/(1/Time.deltaTime)));
			} else {
				charAnim.speed = 1;
			}

			*/
			if (charAnim.GetInteger ("state") != 9) { //if not falling
				if ((stepCountAnim > stepAnimThresh) && (Vector3.Distance (transform.position, blade.position) > 4.5f)) { //make the figure run when near the blade

					charAnim.SetInteger ("state", 0);

					charAnim.speed =1;
					Debug.Log ("anim speed = RESET");


				} else {

					charAnim.SetInteger ("state", 1);
				}
			}


		} else {
			if (controlMat.color.a < 1) {
				controlMat.color += new Color (0, 0, 0, .05f);


				controlMat2.color += new Color (0, 0, 0, .05f);

				if (Reset.resetCount > 0) {
					highScore.GetComponent<Text> ().color = controlMat.color;
					highScore2.GetComponent<Text> ().color = controlMat.color;
					hs.GetComponent<Text> ().color = new Color (hs.GetComponent<Text> ().color.r, hs.GetComponent<Text> ().color.g, hs.GetComponent<Text> ().color.b, controlMat.color.a);
					hs2.GetComponent<Text> ().color = new Color (hs2.GetComponent<Text> ().color.r, hs2.GetComponent<Text> ().color.g, hs2.GetComponent<Text> ().color.b, controlMat.color.a);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D myCol){
		if (!beginPush) {
			beginPush = true;
			//Debug.Log ("FIRST COLLISION");
			charAnim.SetInteger ("state", 0);
		}
	}

	void OnTriggerStay2D(Collider2D myTrig){
		//Debug.Log ("triggered");
		if (myTrig.gameObject.tag == "item") {
			audioContainer2.PlayOneShot (getItem, 1f);
			CurrencyGen.score++;
			Reset.resetCount = 0;
			Reset.scoreAtLoss = CurrencyGen.score;

			if (CurrencyGen.score > PlayerPrefs.GetInt("realHighScore")) {
				PlayerPrefs.SetInt ("realHighScore", CurrencyGen.score);
			}
			Destroy (myTrig.gameObject);
		}
	}
}
