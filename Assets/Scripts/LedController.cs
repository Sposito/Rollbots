using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedController : MonoBehaviour {

	public float density = 0.5f;
	Color color; 
	Renderer myRenderer;
	public Color currentColor;
	float asyncStart = 0f;
	float frequency = 1f;
	void Awake () {
		if(Random.Range(0f,1f) > density)
			Destroy (gameObject,0f);
	}

	void Start(){
		color = Random.Range(0f,1f) > 0.5f ? Color.red: Color.green;
		myRenderer = GetComponent<Renderer> ();
		myRenderer.material.color = color;
		myRenderer.material.SetColor ("_EmissionColor", color);
		asyncStart = Random.Range (0f, 360f);
		frequency = Random.Range (0f, 5f);
	}
	
	// Update is called once per frame
	void Update () {
		currentColor = Color.Lerp(Color.black,color, Mathf.Sin(Time.time * frequency  + asyncStart));
		myRenderer.material.color = currentColor;
		myRenderer.material.SetColor ("_EmissionColor", currentColor);
	}
}
