using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

	public bool reset = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.name + " triggered");

		switch (other.name){
			case "box":
				if (reset)
				Application.LoadLevel (Application.loadedLevelName);
				break;
			case "SliceSound":
				Debug.Log ("slice sound");
				other.gameObject.GetComponent<AudioSource> ().Play ();
				break;
		}
	}
}
