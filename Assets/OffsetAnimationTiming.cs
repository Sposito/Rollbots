using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetAnimationTiming : MonoBehaviour {

    Animator anim;
    AudioSource source;
	void Start () {
        anim = GetComponent<Animator>();
        anim.ForceStateNormalizedTime(Random.Range(0f, 1f));
        source = GetComponent<AudioSource>();
        //anim.Play()
	}
	
    public void PlaySound(){
        source.Play();
    }
}
