using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script used by camera to follow the player
/// </summary>
public class CameraController : MonoBehaviour {
	public float smoothness = 5f;
	GameObject target;
	public Vector3 offset;
	public bool isUpponPlayer = false;
	public Vector3 initialOfset = Vector3.zero;

	void Start () {
		
		target = GameObject.FindGameObjectWithTag ("Player");
			
		//transform.position = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z);
		MoveUpponPlayer();
		offset = transform.position - target.transform.position;
	}

	void MoveUpponPlayer(){
		if (isUpponPlayer) {
			transform.position = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z) + initialOfset;
			transform.LookAt (target.transform);
		}
		
	}
	void Update () {
			transform.position = Vector3.Lerp (transform.position, target.transform.position + offset, smoothness * Time.deltaTime);
		
	}


}


