using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadGame : MonoBehaviour {

    public Slider bar;
	void Start () {
        StartCoroutine(Load());
	}
	
    IEnumerator Load(){
        yield return new WaitForSeconds(3f);
        var operation = SceneManager.LoadSceneAsync(1);
        while(!operation.isDone){
            print(bar.value);
            bar.value = operation.progress;
            yield return null;
        }
    }
}
