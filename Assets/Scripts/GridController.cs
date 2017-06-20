using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour {
	#if UNITY_EDITOR
	public bool isEditor = false;
	#endif


    public static GridController singleton;
	GameObject tile;
	public GameObject playerCube;
	public GameObject mainCamera;

	public GameObject[] tilesGO;
	const float floorHeight = -0.5f;
	public static GameMap gameMap = new GameMap();

	public SideIconsController iconsUI;
	public Slider energyUI;

	public int chunkSideSize = 16;

	public MovementResponse MovePlayer(Direction dir){

		if (gameMap.player.energy < 1) {
            gameMap.EnergyOver();
			energyUI.value = gameMap.player.energy;
			return MovementResponse.blocked;
		}

		MovementResponse response = gameMap.MovePlayer (dir);
		//print (gameMap.player.faces);
		iconsUI.UpdateFaces (gameMap.player.faces);
		energyUI.value = gameMap.player.energy;


		return response;
	}


	void DinamicLoadChunks(){
		//gameMap.player(
	}
	void Start () {
		chunkSideSize = 16;
		energyUI.maxValue = gameMap.player.totalEnergy;
		print("Camera");
		LoadMap ();

		BuildCube ();

        GameObject _camera = Instantiate (mainCamera);
		print (_camera.gameObject.transform.position);

		for(int i = 0; i < transform.childCount; i++){
			transform.GetChild (i).gameObject.GetComponent<DinamicQuadLoader> ().Init ();
		}

        singleton = this;

	}

	void LoadMap(){
		int width = gameMap._map.GetLength (0);
		int height = gameMap._map.GetLength (1);
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < width; j++) {
				gameMap._map [i, j].LoadFromFolder ();
				//if(i == 2 && j ==2)
					LoadChunk (gameMap._map [i, j]);
			}
		}
	}

	void LoadChunk(Chunk c){
		GameObject chunkGO = new GameObject("Chunk " + c.xInMap + ", " + c.yInMap);
		float side = (float)chunkSideSize;
		chunkGO.transform.position = new Vector3 (c.xInMap * side + side/2, floorHeight, c.yInMap * side + side/2);

		chunkGO.transform.SetParent (transform);
		chunkGO.AddComponent<DinamicQuadLoader> ();


		float offset = 4f;
		GameObject quadI = new GameObject ("I");
		quadI.transform.SetParent(chunkGO.transform);
		quadI.transform.localPosition = new Vector3 (offset, 0, offset);

		GameObject quadII = new GameObject ("II");
		quadII.transform.SetParent(chunkGO.transform);
		quadII.transform.localPosition = new Vector3 (-offset, 0, offset);

		GameObject quadIII = new GameObject ("III");
		quadIII.transform.SetParent(chunkGO.transform);
		quadIII.transform.localPosition = new Vector3 (-offset, 0, -offset);

		GameObject quadIV = new GameObject ("IV");
		quadIV.transform.SetParent(chunkGO.transform);
		quadIV.transform.localPosition = new Vector3 (offset, 0, -offset);

		Transform t = quadI.transform;

		for (int i = 0; i < c.sideSize; i++) {
			for (int j = 0; j < c.sideSize; j++) { 
				float x = c.xInMap * chunkSideSize + (float)i;
				float y = c.yInMap * chunkSideSize + (float)j;
				Vector3 pos = new Vector3 (x, floorHeight, y);
				int id = c.GetTileIdByPos (i, j);
				GameObject go = (GameObject)Instantiate (tilesGO [id],pos , Quaternion.identity);
				#if UNITY_EDITOR
				if (isEditor) {
					BoxCollider col = go.AddComponent<BoxCollider> ();
					col.isTrigger = true;
					//if(col.size.x <= 2)
						//col.size = Vector3.one;
					TileEditorBehaviour teBehaviour = go.AddComponent<TileEditorBehaviour> ();
					teBehaviour.chunk = c;

					teBehaviour.x = i;
					teBehaviour.y = j;
				}
				#endif
				bool halfX = (i < c.sideSize / 2);
				bool halfY = (j < c.sideSize / 2);

				if (!halfX && !halfY) {
					t = quadI.transform;
				}
				else if (halfX && !halfY) {
					t = quadII.transform;
				}
				else if (halfX && halfY) {
					t = quadIII.transform;
				}
				else if (!halfX && halfY) {
					t = quadIV.transform;
				}

				go.transform.SetParent (t);


			}
		}
	}

	/// <summary>
	/// Instatiates the  Player and adds its behaviour.
	/// </summary>
	public void BuildCube(){
		GameObject cube = (GameObject)Instantiate (playerCube, new Vector3(gameMap.player.position.x + 0f,0f,gameMap.player.position.y), Quaternion.identity);
		cube.AddComponent<CubeController> ();

	}
		


}
