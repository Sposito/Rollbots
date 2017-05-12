using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
	public Face[] faces;
	public Position position { get { return pos; }}
	Position pos;
	public int totalEnergy = 50;
	public int energy;
	public void SetPosition(Position pos){
		this.pos = pos;
	}
	public Player (){
		// 2 3 5 6 1 4
		energy = totalEnergy;
		faces =  new []{Face.smallCircle, Face.smallCross, Face.bigCircle, Face.bigCross,Face.smallSquare, Face.bigSquare };
		pos = new Position (8, 8);
	}

	public Player (int x, int y){
		// 2 3 5 6 1 4
		energy = totalEnergy;
		faces =  new []{Face.smallCircle, Face.smallCross, Face.bigCircle, Face.bigCross,Face.smallSquare, Face.bigSquare };
		pos = new Position (x, y);
	}

}