using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresentationController : MonoBehaviour {

    public GameObject[] slides;
    int current = 0;
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonUp("Next") || Input.GetKeyUp(KeyCode.S)) {
            Next();
        }
        else if (Input.GetButtonUp("Previous") || Input.GetKeyUp(KeyCode.A)) {
            Prev();
        }
    }

    void Next(){
        slides[current].SetActive(false);
        if(current < slides.Length-1)
            current++;

        slides[current].SetActive(true);
    }

    void Prev(){
		slides[current].SetActive(false);
		if (current > 0)
			current--;
        
        slides[current].SetActive(true);
    }

}
