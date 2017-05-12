using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker{

	public static  Face[] Walk( Face[] faceState, Direction dir){
		if (faceState.Length != 6) {
			throw new UnityException ("Player must have 6 faces.");
		}

		Face[] result = new Face[6];
		switch (dir) {
		case Direction.North:
			result = new []{ faceState [1], faceState [2], faceState [3], faceState [0], faceState [4], faceState [5] };
			break;
		case Direction.South:
			result = new []{ faceState [3], faceState [0], faceState [1], faceState [2], faceState [4], faceState [5] };
			break;
		case Direction.East:
			result = new []{ faceState [5], faceState [1], faceState [4], faceState [3], faceState [0], faceState [2] };
			break;
		case Direction.West:
			result = new []{ faceState [4], faceState [1], faceState [5], faceState [3], faceState [2], faceState [0] };
			break;
		}
		return result;

	}


	public static Face GetGroundedFace(Player player, Direction dir){
		return Walk (player.faces, dir) [0];
	}
}