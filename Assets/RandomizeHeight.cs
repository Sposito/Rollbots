using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeHeight : MonoBehaviour {

	// Use this for initialization

	public float maxVariance = 1.2f;
	void Start () {
		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, Random.Range (transform.localScale.z, transform.localScale.z * maxVariance));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
