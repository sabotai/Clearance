using UnityEngine;
using System.Collections;

public class MoveCurrency : MonoBehaviour {

	public float speed = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float newX =transform.position.x - (Time.deltaTime * speed);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z) ;
	}


}
