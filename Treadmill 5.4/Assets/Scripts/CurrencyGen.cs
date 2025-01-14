﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrencyGen : MonoBehaviour {

	public GameObject itemPrefab, itemPrefab2, currentItem;
	public Transform spawnPt;
	public int spawnRate = 5;
	private float nextSpawn;

	private bool coin = false;
	public static int score = 0;
	public static int prevScore;

	GameObject textObj;
	private Vector3 originalScale;

	public Material mat;

	// Use this for initialization
	void Start () {
		nextSpawn = spawnRate;

		if (textObj == null) {
			//Debug.Log ("manually assigning");
			textObj = GameObject.Find ("Text");
			originalScale = textObj.GetComponent<RectTransform> ().localScale;
		}

	}
	
	// Update is called once per frame
	void Update () {


		int oldScore = int.Parse(textObj.GetComponent<Text> ().text);

		if (oldScore != score) {
			textObj.GetComponent<RectTransform> ().localScale = originalScale;
			mat.color = new Color(mat.color.r, mat.color.g,mat.color.b, 1);
		} else {
			if (score != 0) {
				float temp = 60/(1/Time.deltaTime); // pct of 60fps
				//Debug.Log("temp = " + temp);
					textObj.GetComponent<RectTransform> ().localScale *= (1-(0.01f * temp));
					mat.color = new Color (mat.color.r, mat.color.g, mat.color.b, mat.color.a * (1-(0.05f * temp)));//(0.05f * (60/(1/Time.deltaTime))));
			}
		}
		textObj.GetComponent<Text>().text = score.ToString();
	



		if (Time.time > nextSpawn) {
			nextSpawn = Time.time + spawnRate;

			if (MoveForward.beginPush) {

				Debug.Log ("next spawn = " + nextSpawn);
				if (coin) {
					currentItem = itemPrefab;
					Debug.Log ("current - 1");

				} else {
					currentItem = itemPrefab2;
					Debug.Log ("current - 2");
				}
				coin = !coin;

					GameObject clone = Instantiate (currentItem, spawnPt.position, Quaternion.identity) as GameObject;
					Debug.Log ("make money");

			}



		} 
	}
}
