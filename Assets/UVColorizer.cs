using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVColorizer : MonoBehaviour {

	MeshFilter mesh;
	static bool first = true;
	bool doit = false;
	void Start () {
		if (first) {
			first = false;
			doit = true;
		}

		mesh = GetComponent<MeshFilter> ();
		List<Vector2> uvs = new List<Vector2>();
		mesh.mesh.GetUVs (0, uvs);
		for (int i = 0; i < uvs.Count; i++) {
			if (doit){
				print (uvs[i]);
			}
			uvs[i] += Vector2.right * 0.25f;
			if (doit){
				//print (uvs[i]);
			}
		}
		mesh.mesh.SetUVs (0, uvs);
		mesh.mesh.UploadMeshData (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
