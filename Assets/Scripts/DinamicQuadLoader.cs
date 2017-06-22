using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinamicQuadLoader : MonoBehaviour {

	GameObject[] quads = new GameObject[4];
	float distThreashold = 16f;
	Transform player;

	public void Init (){
		player = GameObject.FindWithTag ("Player").transform;


		quads[0] = transform.Find ("I").gameObject;
		quads[1] = transform.Find ("II").gameObject;
		quads[2] = transform.Find ("III").gameObject;
		quads[3] = transform.Find ("IV").gameObject;

		StartCoroutine("CheckVisibility");

	}


	void FlipFlop(){
		float dist = 100f;
		foreach (GameObject go in quads) {
			dist = Vector3.Distance (go.transform.position, player.position);
			//print (dist);
			if (dist <= distThreashold) {
				go.SetActive (true);
			} else if (go.activeSelf) {
				go.SetActive (false);
			}
		}
	}

	IEnumerator CheckVisibility(){
		while (true) {
			FlipFlop ();
			yield return new WaitForSeconds(1f);
		}
	}
}
