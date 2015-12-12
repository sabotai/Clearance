using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

	public bool reset = true;
	public Vector3 pipePos2, pipePos1;
	GameObject pipe;
	bool pipePresent = true;
	bool pipeOVER = false;

	// Use this for initialization
	void Start () {
		pipe = GameObject.Find ("Pipe");


		if (pipe.transform.position == pipePos2) {
			pipePresent = true;
		} else if (pipe.transform.position == pipePos1) {
			pipePresent = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!pipeOVER) {
			pipeEntry ();
		}


		if (Input.GetKeyDown (KeyCode.R)) {

			Application.LoadLevel (Application.loadedLevelName);
		}
	}

	void pipeEntry(){

		float pipePct = Mathf.Clamp (Mathf.Sin (Time.time), -0.5f, 0) * -2;


		//Debug.Log ("moving pipe by " + pipePct);

		if (pipePct <= 0.99f) { //stop when 99% of journey crossed
			if (pipePresent) {
				pipe.transform.position = Vector3.Lerp (pipePos2, pipePos1, pipePct);
			} else if (1 - pipePct < 0.99f) {
				pipe.transform.position = Vector3.Lerp (pipePos1, pipePos2, 1 - pipePct);
				Debug.Log ("removing pipe");
			} else {
				pipeOVER = true;
				Debug.Log ("pipe stuff is OVER");
			}
		} else {
			pipePresent = false;
			if (GetComponent<Rigidbody2D> ().isKinematic)
				pipe.GetComponent<AudioSource> ().Play ();
			GetComponent<Rigidbody2D> ().isKinematic = false;
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.name + " triggered");

		switch (other.name){
			case "box":
				if (reset)
				Application.LoadLevel (Application.loadedLevelName);
				break;
			case "SliceSound":
				other.gameObject.GetComponent<AudioSource> ().Play ();
			break;
		case "Blade":

			if (!other.gameObject.GetComponent<ParticleSystem> ().isPlaying) {
				other.gameObject.GetComponent<ParticleSystem> ().Play ();
				MoveForward.antForceDiv /= 2;
				StartCoroutine (CameraShake.Shake (0.2f, 0.1f));

				CurrencyGen.score = 0;
			}
			break;
				
		}
	}
}
