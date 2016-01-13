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
				textObj.GetComponent<RectTransform> ().localScale *= 0.99f;
				mat.color = new Color (mat.color.r, mat.color.g, mat.color.b, mat.color.a * 0.95f);
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
