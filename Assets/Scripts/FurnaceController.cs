using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceController : MonoBehaviour {

	Light myLight;
	float baseRange;
	float baseIntensity;
	public float amount = 4f;
	float rndStart;
    public float mutltiplier = 100f;
	void Start () {
		myLight = GetComponentInChildren<Light> ();
		baseRange = myLight.range;
		baseIntensity = myLight.intensity;
        rndStart = Random.Range (0f, mutltiplier);
	}
	
	void Update () {
		myLight.range = baseRange + (Mathf.Sin (Time.time + rndStart) + 1) / 2;
		myLight.intensity = baseIntensity + (Mathf.Sin (Time.time + rndStart) + 1) / 2;
	}
}
