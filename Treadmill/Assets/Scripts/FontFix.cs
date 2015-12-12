using UnityEngine;
using System.Collections;

public class FontFix : MonoBehaviour {

	public Font myFont;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		myFont.material.mainTexture.filterMode = FilterMode.Point;
	}
}
