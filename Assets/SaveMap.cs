using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SaveMap : MonoBehaviour {

	string saveFolder;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKeyUp (KeyCode.S)) {
			print ("Saved.");
			Save ();
		}
		
	}

	void Save(){
		foreach (Chunk c in GridController.gameMap._map) {
			File.WriteAllText (Application.dataPath + "/Resources/Maps/" + c.xInMap + "_" + c.yInMap + ".json", JsonUtility.ToJson(c));


		}

		#if UNITY_EDITOR
		AssetDatabase.Refresh ();
		#endif
	}
}
