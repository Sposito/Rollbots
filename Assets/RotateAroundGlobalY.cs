using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundGlobalY : MonoBehaviour {

	bool rot = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (rot)
			transform.Rotate (Vector3.up, Space.World);
	}
}
