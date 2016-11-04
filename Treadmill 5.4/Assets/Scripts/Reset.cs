using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour {

	public bool reset = true;
	public Vector3 pipePos2, pipePos1;
	GameObject pipe;
	public bool pipePresent = true;
	public bool pipeOVER = false;
	Animator charAnim;

	public static int resetCount = 0; //keeps track of whether to show the instructions or not
	public GameObject instructions;

	public static int scoreAtLoss = 0;

	bool scoreDown;

	private int frameCounter = 0;

	// Use this for initialization
	void Start () {
		pipe = GameObject.Find ("Pipe");

		if (pipe.transform.position == pipePos2) {
			pipePresent = true;
		} else if (pipe.transform.position == pipePos1) {
			pipePresent = false;
		}

		charAnim = GetComponent<Animator> ();
		pipeOVER = false;
		pipePresent = true;
		scoreAtLoss = 0;



		if (resetCount > 3) {
			instructions.transform.position = new Vector3 (4.85f, -4.85f, 0);
		}
		if (resetCount > 1) {
			instructions.SetActive(true);
		}

		scoreDown = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!pipeOVER) {
			pipeEntry ();
		}


		if (Input.GetKeyDown (KeyCode.Space)) {

			SceneManager.LoadScene (0);
			//Application.LoadLevel (Application.loadedLevelName);
		}

		if (scoreDown && CurrencyGen.score > 0){
			if (frameCounter == 0) { //frameCounter determines how quickly the score counts down
				CurrencyGen.score--;

				GameObject.Find ("Character").GetComponent<MoveForward> ().audioContainer2.PlayOneShot (GameObject.Find ("Character").GetComponent<MoveForward> ().getItem, 1f);

			} else {
				
			}
		} else {
			scoreDown = false;
		}


		if (CurrencyGen.score == 0) {
			if ((scoreAtLoss > 0) && (!GameObject.Find ("Character").GetComponent<MoveForward> ().audioContainer2.isPlaying)) {
				scoreAtLoss = 0;
				SceneManager.LoadScene (0);
			}
		}



		if (frameCounter < 2){ //lower the score every 2 frames
			frameCounter++;
		} else {
			frameCounter = 0;
		}
	}

	void pipeEntry(){

		float pipePct = Mathf.Clamp (Mathf.Sin (Time.timeSinceLevelLoad), -0.5f, 0) * -2;


		//Debug.Log ("moving pipe by " + pipePct);

		if (pipePct <= 0.99f) { //stop when 99% of journey crossed
			if (pipePresent) {
				pipe.transform.position = Vector3.Lerp (pipePos2, pipePos1, pipePct);
			} else if (1 - pipePct < 0.99f) {
				pipe.transform.position = Vector3.Lerp (pipePos1, pipePos2, 1 - pipePct);
				//Debug.Log ("removing pipe");
			} else {
				pipeOVER = true;
				//Debug.Log ("pipe stuff is OVER");
			}
		} else {
			pipePresent = false;
			if (GetComponent<Rigidbody2D> ().isKinematic) {
				pipe.GetComponent<AudioSource> ().Play ();
			}
			GetComponent<Rigidbody2D> ().isKinematic = false;
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log (other.name + " triggered");

		switch (other.name){
		case "box":
			if (reset) { //reset is only always on or off for debug
				Debug.Log ("reset game #" + resetCount);
				if (CurrencyGen.score > 0) {
					Reset.resetCount = 0;
				} else {
					resetCount++;
				}
				//GetComponent<MoveForward>().controlMat2.color = new Color(1,1,1,1);

				//	SceneManager.LoadScene (0);		
				if (CurrencyGen.score <= 0) {
					SceneManager.LoadScene (0);
				}
				

				//Application.LoadLevel (Application.loadedLevelName);
			}
					break;
		case "SliceSound":
			if (!other.gameObject.GetComponent<AudioSource> ().isPlaying) {
				other.gameObject.GetComponent<AudioSource> ().Play ();
			}
				break;
		case "Blade":

			if (!other.gameObject.GetComponentInChildren<ParticleSystem>().isPlaying){
				//GetComponent<ParticleSystem> ().isPlaying) {

				other.gameObject.GetComponentInChildren<ParticleSystem>().Play ();
			
			//if (!other.gameObject.GetComponent<ParticleSystem> ().isPlaying) {

				//other.gameObject.GetComponent<ParticleSystem> ().Play ();
			//}
					MoveForward.antForceDiv /= 2;
					StartCoroutine (CameraShake.Shake (0.2f, 0.1f));

					if (!scoreDown){
						CurrencyGen.prevScore = CurrencyGen.score;
						scoreDown = true;

						//CurrencyGen.score = 0;
					}
				}
				break;


		case "BladeF":

				if (!other.gameObject.GetComponent<ParticleSystem> ().isPlaying) {
					other.gameObject.GetComponent<ParticleSystem> ().Stop ();
					other.gameObject.GetComponent<ParticleSystem> ().Play ();
					MoveForward.antForceDiv /= 2;
					StartCoroutine (CameraShake.Shake (0.2f, 0.1f));
				}
			break;
			case "sliced":
				//Debug.Log ("sliced");
				charAnim.SetInteger ("state", 9);
				break;
				
		}

	}

	/*
	void OnApplicationQuit(){

		GameObject.Find("CurrencyGenerator").GetComponent<CurrencyGen>().mat.color = new Color(1,1,1,1);
	}
*/

}
