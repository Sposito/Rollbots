using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chunk{


	public int xInMap;
	public int yInMap;
	public int sideSize = 16;
	public int[] flatMap;
	public int[,] map;

	public Chunk(int x, int y){
		map = new int[sideSize, sideSize];
		xInMap = x;
		yInMap = y;
	}

	public int GetTileIdByPos(int x, int y){
		return map[x,y];
	}

	public void SetIdByPos(int x, int y, int id){
		map [x, y] = id;
		FlatMap ();
	}

	void FlatMap(){
		flatMap = new int[sideSize * sideSize];
		for (int i = 0; i < map.GetLength (0); i++)
			for (int j = 0; j < map.GetLength (1); j++)
				flatMap [i + j * sideSize] = map [i, j];	
	}

	void UnflatMap(){
		map = new int[sideSize, sideSize];
		for (int i = 0; i < map.GetLength (0); i++)
			for (int j = 0; j < map.GetLength (1); j++)
				map [i, j] = flatMap [i + j * sideSize];
	}

	public void FillFlat(){
		for (int i = 0; i < map.GetLength(0); i++)
			for (int j = 0; j < map.GetLength(1); j++)
				map [i,j] = 0;
	}

	public void FillRandom(float emptyRate){

		for (int i = 0; i < map.GetLength (0); i++)
			for (int j = 0; j < map.GetLength (1); j++) {
				map [i, j] = (Random.Range (0f, 1f) <= emptyRate) ? 0 : Random.Range (1, 11);
				if (map [i, j] >= 7)
					map [i, j] += 94;
			}

		int x = sideSize / 2;
		int y = x + 1;
		map [x, x] = 0;
		map [x, y] = 0;
		map [y, x] = 0;
		map [y, y] = 0;

	}
	public void LoadFromFolder(){
		TextAsset textAsset = Resources.Load ("Maps/" + xInMap + "_" + yInMap) as TextAsset;
		LoadFromJSON (textAsset.text);
	}

	#region Legacy
	public void LoadFromURL(string url){
		WWW www = new WWW (url);
		while (!www.isDone) {
		}

		LoadFromJSON (www.text);
	}

	public void LoadFromJSON(string json){

		JsonUtility.FromJsonOverwrite (json, this);
		UnflatMap ();
	}


	/// <summary>Serialize the chuck as json file. </summary>
	/// <returns>The Json file.</returns>
	public string ToJson(){
		FlatMap ();
		string result = JsonUtility.ToJson (this);

		// Brake result in two parts and format for easier viewing 
		string[] splitedResult = result.Split (new[]{ "\"flatMap\":" }, System.StringSplitOptions.None);
		splitedResult [0] += "\"flatMap\":";


		string[] brokenValues = splitedResult[1].Split(new []{','});
		Debug.Log (brokenValues.Length);
		result = splitedResult [0]; 
		string r = "[\n";
		brokenValues [0] = brokenValues [0].Remove (0, 1);
		for (int i = 0; i < brokenValues.Length; i++) {

			if (brokenValues [i].Length == 1) {
				brokenValues [i] = "  " +brokenValues [i];
			}

			r += brokenValues [i] + ",  ";
			if ((i+1 )% sideSize == 0 && i >= sideSize -1)
				r += "\n";
		}



		r = r.Remove (r.Length - 5,5);
		r+= "}";
		return splitedResult [0] + r;
	}
	#endregion

}