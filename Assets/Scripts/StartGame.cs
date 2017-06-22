using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public void StartMyGame(){
		print (transform.position.y);
		if (transform.position.y > 1100) {
			StartCoroutine ("WaitAndLoad");
		}
			
	}

	IEnumerator WaitAndLoad(){
		yield return new WaitForSecondsRealtime (1f);
		SceneManager.LoadScene (1);

	}
}
