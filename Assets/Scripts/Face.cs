using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face{
	public static Face smallCircle = new Face (FaceKind.smallCircle);
	public static Face bigCircle = new Face (FaceKind.bigCircle);
	public static Face smallSquare = new Face (FaceKind.smallSquare);
	public static Face bigSquare = new Face (FaceKind.bigSquare);
	public static Face smallCross = new Face (FaceKind.smallCross);
	public static Face bigCross = new Face (FaceKind.bigCross);

	public FaceKind kind;
	public Face( FaceKind kind){
		this.kind = kind;
	}
}