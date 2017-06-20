using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargePadBehaviour : MonoBehaviour {

    Material[] redGlowingThings;
    public Color activeColor;
    AudioSource audioSource;

    bool active = false;
	void Awake () {
        audioSource = GetComponent<AudioSource>();
        MeshRenderer[] meshs = GetComponentsInChildren<MeshRenderer>();
        print(meshs.Length);
        redGlowingThings = new Material[meshs.Length - 1];
        for (int i = 1; i < meshs.Length; i++){
            redGlowingThings[i - 1] = meshs[i].material;
        }
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            audioSource.Play();
            if (!active) {
                foreach (Material m in redGlowingThings) {
                    m.color = activeColor;
                }
                GetComponentInChildren<Light>().color = activeColor;
                active = true;
            }

        }


    }
}
