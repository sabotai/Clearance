using UnityEngine;
using System.Collections;

public class TitlePan : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((Mathf.Clamp (Mathf.Sin (Time.timeSinceLevelLoad), -0.5f, 0) * -2) < 2){
			transform.position -= new Vector3(0.005f,0,0);
		}
	}
}
