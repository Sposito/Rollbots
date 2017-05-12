using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script used by camera to follow the player
/// </summary>
public class FollowCamera : MonoBehaviour {
	public float smoothness = 5f;
	GameObject target;
	public Vector3 offset;

	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player");
		transform.position = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z);
		offset = transform.position - target.transform.position;
	}
	void Update () {
		transform.position = Vector3.Lerp (transform.position, target.transform.position + offset, smoothness * Time.deltaTime);
	}
}
