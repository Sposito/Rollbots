using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

	Transform t;
	Quaternion cacheRot;
	void Start () {
		t = Camera.current.transform;	
	}
	
	// Update is called once per frame
	void Update () {
		if (t != null) {
			cacheRot = transform.rotation;
			transform.LookAt (t);
			transform.rotation = Quaternion.Lerp (cacheRot, transform.rotation, 0.1f);

		} 
		else {
			t = Camera.current.transform;	
		}
	}
}
