using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHeadBehaviour : MonoBehaviour {

	public GameObject head;
	void Start () {
		head = (GameObject)Instantiate (head, transform.position, Quaternion.identity);

		head.transform.localScale *= 0.6f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
