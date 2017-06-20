using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithWind : MonoBehaviour {

    // Use this for initialization
    Rotate rot;
    float baseSpeed;
    public bool isVertical = false;
	void Start () {
        rot = GetComponentInChildren<Rotate>();
        baseSpeed = rot.speed;
	}
	
	// Update is called once per frame
	void Update () {
        if(!isVertical)
            transform.localRotation = Quaternion.Euler(Vector3.forward * WindController.lastWind.eulerAngles.y) ;
        //Quaternion.Euler(Vector3.forward * Mathf.Rad2Deg * Mathf.Atan2(
        //WindController.wind.y, WindController.wind.x));
        rot.speed =  baseSpeed * WindController.WINDPOWER / 2;


	}
}
