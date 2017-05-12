using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class GenerateJsonMaps : Editor {
	
	[MenuItem( "RollBots/Generate JsonFiles" )]
	static void GenerateJsonFiles(){
		int chunkSize = 16;
		Texture2D text = (Texture2D)Resources.Load ("Maps/map16");
		int hChunks = text.width / chunkSize;
		int vChunks = text.height / chunkSize;

		Position head = new Position (0, 0);
		int currentCol = 0;
		int currentRow = 0;

		while (currentCol < hChunks) {
			while (currentRow < vChunks) {
				Chunk chunk = new Chunk (currentCol, currentRow);
				int x = 0;
				int y = 0;
				for(int i = currentCol * chunkSize; i < currentCol * chunkSize + chunkSize; i++){
					for(int j = currentRow * chunkSize; j < currentRow * chunkSize + chunkSize ; j++){
						 
						Color color = text.GetPixel (currentCol * chunkSize + x, currentRow * chunkSize + y);  
						int id = ColorDic.GetIdByColor (color);
						if (id < 100) {
							chunk.map [x, y] = id;
						} 
						else {
							float value = Random.value;
							if (value < 0.6f)
								id = 100;
							else if (value < 0.75f)
								id = 101;
							else if (value < 0.90f)
								id = 102;
							else {
								id = Random.Range (103, 109);
							}

							chunk.map [x, y] = id;
						}

						y++;
					}
					y = 0;
					x++;
				}

				string result = chunk.ToJson ();

				File.WriteAllText (Application.dataPath + "/Resources/Maps/" + currentCol + "_" + currentRow + ".json", result);
				//AssetDatabase.Refresh ();
				currentRow++;
			}
			currentRow = 0;
			currentCol++;
		}

		AssetDatabase.Refresh ();
	}

	void DEfineCOlor(){
	}
}

 class ColorDic{
	static Dictionary<Color,int> translate = new Dictionary<Color,int> {
		{Color.white, 0},
		{Color.black, 100},
		{Color.red, 1},
		{Color.blue, 2},
		{Color.green, 3},
		{Color.yellow, 4},
		{Color.cyan, 5},
		{Color.magenta, 6},
		{(Color)new Color32( (byte)255, (byte)128, (byte)0, (byte)255 ), 7 } //orange

	};

	static public int GetIdByColor(Color color){
		if(color == Color.red)
			Debug.Log("Red");
		try{
			return translate[color];
		}

		catch{
			Debug.LogWarning ("Unregistered color: R: " + color.r + ", G: " + color.g + ", B: " + color.b );

		}

		return 0;
	}
}
