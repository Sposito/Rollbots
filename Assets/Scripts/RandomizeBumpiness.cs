using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBumpiness : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer>().material.SetFloat("_BumpScale",Random.Range(0f,0.7f)); 
		int rnd = Random.Range (0, 4);
		transform.Rotate (Vector3.up * 90f * rnd, Space.World);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
