using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public bool x;
	public bool y;
	public bool z;
	public float speed = 1f;
	Vector3 axis;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		axis = new Vector3 (x ? 1 : 0, y ? 1 : 0, z ? 1 : 0);
		transform.Rotate (axis, speed * Time.deltaTime * 60);
	}
}
