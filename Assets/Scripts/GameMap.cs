using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
#region GameMapEnums
/// <summary>Response given upon movement try. </summary>
public enum MovementResponse{ ok, failed, blocked}; //note that blocked and failed movements are treated diferently

/// <summary>Cardinal Directions. </summary>
public enum Direction{North, South, East, West}

/// <summary>Possible face kinds. </summary>
public enum FaceKind{flat=0, smallSquare=1, smallCircle=2, smallCross=3, bigSquare=4, bigCircle=5, bigCross=6}
#endregion

public class GameMap {
    public int chunnkSideSize = 16;
    int mapSide = 4;

    public static bool isStoringPosition;

	public Dictionary<string,Chunk> map = new Dictionary<string,Chunk> ();

	public Chunk[,] _map; 

	public Player player;
	public Position checkPoint;
	public GameMap(){
		
		player = new Player (32 + 8,16 + 7); //TODO: Get rid of these magical numbers
		//map.Add ("0,0", new Chunk (16,16));
		checkPoint = player.position;
		InitializeChunks();
	}

    public void EnergyOver(){
		player.SetPosition(checkPoint);
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        playerGO.transform.position = new Vector3(player.position.x + 0f, 0f, player.position.y + 0f);
        playerGO.transform.rotation = player.Rotation;
        player.RestoreRotation();
		player.energy = player.totalEnergy;
        
    }

	void InitializeChunks(){
		_map = new Chunk[mapSide, mapSide];
		for (int i = 0; i < mapSide; i++) {
			for (int j = 0; j < mapSide; j++) {
				_map [i, j] = new Chunk (i, j);
			}
		}
	}


	 
	/// <summary>
	/// Attemp to moves the player in a given direction.
	/// </summary>
	/// <returns>The player.</returns>
	/// <param name="dir">Direction.</param>
	public MovementResponse MovePlayer(Direction dir){
		
		Position pos = new Position (player.position.x, player.position.y);
		switch(dir){
			case Direction.North:
				pos.y += 1;
				break;
			case Direction.South:
				pos.y -= 1;
				break;
			case Direction.East:
				pos.x += 1;
				break;
			case Direction.West:
				pos.x -= 1;
				break;
		}

		int tile = GetTile (pos);
		if (tile >= 100) { // All tile codes above 100 are meant to block player movement
			// TODO: If code become messy using objects or struct to encapsulation would be grand,
			// for now we can stay away of this oo burocracy

			return MovementResponse.blocked; // If blocked code path ends in this return
		}



		//stores in this varible the tilecode in the intended tile
		int matchTile =(int) Walker.GetGroundedFace (player, dir).kind;

		if (tile == matchTile || tile == 0 ) {
			
			player.SetPosition(pos);
			Debug.Log (pos.x + ", " + pos.y);
			Debug.Log (player.position.x + " " + player.position.y);
			player.faces = Walker.Walk (player.faces, dir);
			Debug.Log ("Robot: " + matchTile + ", " + "Floor: " + tile );
			player.energy -= 1;
			return MovementResponse.ok;

		}

		else if (tile == 7) { //Recharge Case

           // Debug.Log("recharged");
            checkPoint = pos;
			player.SetPosition(pos);
			Debug.Log (pos.x + ", " + pos.y);
			Debug.Log (player.position.x + " " + player.position.y);
			player.faces = Walker.Walk (player.faces, dir);
			Debug.Log ("Robot: " + matchTile + ", " + "Floor: " + tile );
			player.energy = player.totalEnergy;

			return MovementResponse.ok;

		}

		else {
			Debug.Log ("Robot: " + matchTile + ", " + "Floor: " + tile );
			player.energy -= 2;
			return MovementResponse.failed;
		}
	}

	/// <summary>
	/// Gets the tile in a given pair of coordinates.
	/// </summary>
	/// <returns>The tile.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// 
	int GetTile(int x, int y){
		int mapX = x / chunnkSideSize;
		int mapY = y / chunnkSideSize;

		int chunkX = x % chunnkSideSize;
		int chunkY = y % chunnkSideSize;

		return _map [mapX, mapY]
			.GetTileIdByPos (chunkX,chunkY);
	}

	/// <summary>
	/// Gets the tile in a given position.
	/// </summary>
	/// <returns>The tile.</returns>
	/// <param name="pos">Position in the grid map</param>
	int GetTile(Position pos){
		return GetTile (pos.x, pos.y);
	}
}



