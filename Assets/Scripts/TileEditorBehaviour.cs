using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEditorBehaviour : MonoBehaviour {
	public int x= 0;
	public int y= 0;

	public Chunk chunk;
	ObjectList objectList;
	MeshRenderer[] rend;
	Color grey;
	public int currentID = 11;
	int maxID;

	void Start () {
		
		rend = new MeshRenderer[ gameObject.GetComponentsInChildren<MeshRenderer> ().Length];
		rend = GetComponentsInChildren<MeshRenderer> ();
		grey = rend[0].material.color;
		objectList =(ObjectList) Resources.Load ("Props");
		int maxID = objectList.props.Length;

	}
	
	// Update is called once per frame
	void Update () {

	}




	void OnMouseOver(){
		foreach (MeshRenderer r in rend) {
			r.material.color = Color.cyan;
		}

	}

	void OnMouseExit(){
		foreach (MeshRenderer r in rend) {
			r.material.color = grey;
		}
	}
	void OnMouseDown(){
		int init = 0;
		maxID = objectList.props.Length;
	
		int objID = currentID++;
		if (Input.GetKey(KeyCode.T)){
			maxID = 7;
		}

		if (Input.GetKey (KeyCode.P)) {
			init = 8;

		}

		if (objID >= maxID) {
			objID = init;
			currentID = init;
		}

		if (Input.GetKey (KeyCode.B))
			objID = 8;
		else if (Input.GetKey (KeyCode.R))
			objID = 7;
		GameObject go = (GameObject)GameObject.Instantiate (objectList.props [objID], transform.position, Quaternion.identity);
		TileEditorBehaviour te = go.AddComponent<TileEditorBehaviour> ();
		te.x = x;
		te.y = y;
		te.currentID = currentID;
		te.chunk = chunk;
		go.AddComponent<BoxCollider> ().isTrigger = true;

		try{
			print (string.Format ("Chunk {0}, {1} at position {2},{3}", chunk.xInMap, chunk.yInMap, x, y));
			chunk.SetIdByPos(x,y, objID <= 7? objID: objID + 92);
		}
		finally{
			Destroy (gameObject);
		}


	}
}
