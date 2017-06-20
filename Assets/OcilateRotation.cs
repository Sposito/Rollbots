using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcilateRotation : MonoBehaviour {

    // Use this for initialization
    public AnimationCurve curve;
	public bool x;
	public bool y;
	public bool z;
	public float speed = 1f;
	protected Vector3 axis;
    float randomizer;
	void Start() {
		axis = new Vector3(x ? 1 : 0, y ? 1 : 0, z ? 1 : 0);
        randomizer = Random.Range(0f, speed);
	}

    void Update(){
        RotateIt();
    }

    void RotateIt(){
        
        transform.Rotate((curve.Evaluate(Time.time + randomizer) * axis) / speed);
    }
}
