using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowTrueScore : MonoBehaviour {

	GameObject high, high2;

	// Use this for initialization
	void Start () {
		high = GameObject.Find ("Text-hs2 (1)");
		high2 = GameObject.Find ("Text-hs (1)");

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Tab)) {
			high2.GetComponent<Text> ().text = PlayerPrefs.GetInt ("realHighScore").ToString();
			high.GetComponent<Text> ().text = CurrencyGen.prevScore.ToString();
		} else {
			high2.GetComponent<Text> ().text = "0";
			high.GetComponent<Text> ().text = "0";
		}

	}
}
