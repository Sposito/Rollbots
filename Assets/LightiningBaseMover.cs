using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightiningBaseMover : MonoBehaviour {

	// Use this for initialization
	Vector2 rndStart;
	void Start () {
		rndStart = new Vector2 (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f));
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 v = Random.insideUnitCircle;
		transform.Translate (v.x * Time.deltaTime, 0f, v.y * Time.deltaTime);
		if (transform.position.x > 0.3f)
			transform.position = new Vector3 (0.3f, transform.position.y, transform.position.z);
		else if (transform.position.x < -0.3f)
			transform.position = new Vector3 (-0.3f, transform.position.y, transform.position.z);

		if (transform.position.z > 0.3f)
			transform.position = new Vector3 (transform.position.x, transform.position.y, 0.3f);
		else if (transform.position.z < -0.3f)
			transform.position = new Vector3 (transform.position.x, transform.position.y, -0.3f);
	}
}
