using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SideIconsController : MonoBehaviour {
	public Image north;
	public Image east;
	public Image south;
	public Image west;

	public Sprite [] icons;

	public float lerpSpeed = 0.1f;
	public Color baseColor = Color.red;
	bool fading = false;

	public void UpdateFaces(Face[] state){

		//FadeBack ();
		north.sprite = icons[(int) state [1].kind -1];
		east.sprite  = icons[(int) state [5].kind -1];
		south.sprite = icons[(int) state [3].kind -1];
		west.sprite  = icons[(int) state [4].kind -1];



	}

	public void FadeAway(){
		StartCoroutine (FadeAwayCoroutine ());	
	}

	IEnumerator FadeAwayCoroutine(){
		fading = true;
		Color target = new Color (baseColor.r, baseColor.g, baseColor.b, 0f);
		while (north.color.a >= 0) {
			north.color   = Color.Lerp (baseColor, target, lerpSpeed);
			east.color  = north.color;
			south.color = north.color;
			west.color  = north.color ;

			yield return new WaitForEndOfFrame ();
		}
		fading = false;
	}

	public void FadeBack(){
		StartCoroutine (FadeBackCoroutine());
	}

	IEnumerator FadeBackCoroutine(){
		
		Color from = new Color (north.color.r, north.color.g, north.color.b, 0f);
		while (north.color.a <= baseColor.a) {
			north.color   = Color.Lerp (from, baseColor, lerpSpeed);
			east.color = north.color;
			south.color = north.color;
			west.color  = north.color ;

			yield return new WaitForEndOfFrame ();
		}

		yield return new WaitForSecondsRealtime (4f);
		FadeAway ();
	}
	// Use this for initialization
	void Start () {
		north.color = baseColor;
		east.color  = baseColor;
		south.color = baseColor;
		west.color  = baseColor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
