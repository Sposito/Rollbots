using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour {

	public Vector3 rotation = new Vector3(-90f,0f,0f);
	void Awake () {
		transform.rotation = Quaternion.Euler (rotation);
	}

}
