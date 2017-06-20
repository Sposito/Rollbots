using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script used by camera to follow the player
/// </summary>
public class FollowPlayer : MonoBehaviour {
	public float smoothness = 5f;
	GameObject target;
	public Vector3 offset;
	public bool Async = false;
	bool found = false;

    void Start() {
        if (Async)
            StartCoroutine("FindTarget");
        else {
            target = GameObject.FindGameObjectWithTag("Player");
            found = true;
        }

        if (target != null) {
            transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            offset = transform.position - target.transform.position;
        }
	}
	void Update () {
		if(found)
			transform.position = Vector3.Lerp (transform.position, target.transform.position + offset, smoothness * Time.deltaTime);
	}

	IEnumerator FindTarget(){
		yield return new WaitForSeconds (0.5f);
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			target = GameObject.FindGameObjectWithTag ("Player");
			found = true;
		} else
			FindTarget ();
	}
}
