using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CubeController : MonoBehaviour {
	// Checks if Player is already moving
	bool moving = false;
	//Player RotationSpeed;
	float rotatingSpeed = 300f;

	GridController gridController;
	AudioSource audioSource;
	float playDuration = .8f;

	//float[] pitches = new float[]{1.0f, 1.122462048309373f, 1.2599210498948734f, 1.3348398541700346f, 1.2599210498948734f, 1.122462048309373f, 1.6817928305074297f, 1.122462048309373f, 1.498307076876682f, 1.6817928305074297f, 0.8908987181403392f, 1.498307076876682f, 1.498307076876682f, 1.4142135623730954f, 1.498307076876682f, 1.6817928305074297f, 1.4142135623730954f, 0.9438743126816934f};
	float[] pitches = new float[]{0.2499999999999997f, 0.2499999999999997f, 0.2648657735898235f, 0.2806155120773429f, 0.33370996354250826f, 0.2806155120773429f, 0.33370996354250826f, 0.2806155120773429f, 0.2806155120773429f, 0.2648657735898235f, 0.2499999999999997f, 0.37457676921917005f, 0.2499999999999997f, 0.2499999999999997f, 0.37457676921917005f};
	int current_note = 0;
	void Start(){
		audioSource = GetComponent<AudioSource> ();
		gridController = Component.FindObjectOfType<GridController> ();
		var recognizer = new TKSwipeRecognizer();
		recognizer.triggerWhenCriteriaMet = true;

		recognizer.gestureRecognizedEvent += ( r ) =>
		{
			var direction = r.completedSwipeDirection;
			if (direction == TKSwipeDirection.Down && !moving) {
				MoveSouth ();
			}
			if (direction == TKSwipeDirection.Up && !moving)
				MoveNorth ();

			if (direction == TKSwipeDirection.Right && !moving)
				MoveEast ();

			if (direction == TKSwipeDirection.Left && !moving)
				MoveWest ();

		};
		TouchKit.addGestureRecognizer( recognizer );
	}
	void Update () {
		ReadKeyboardInput ();
	}



	void ReadKeyboardInput (){
		
		if (Input.GetKeyDown (KeyCode.DownArrow) && !moving) {
			MoveSouth ();
		}
		if (Input.GetKeyDown (KeyCode.UpArrow) && !moving)
			MoveNorth ();

		if (Input.GetKeyDown (KeyCode.RightArrow) && !moving)
			MoveEast ();

		if (Input.GetKeyDown (KeyCode.LeftArrow) && !moving)
			MoveWest ();
		
	}

	void MoveSouth(){
		MovementResponse response = gridController.MovePlayer (Direction.South);
		if(response != MovementResponse.blocked){
			bool allowed = response == MovementResponse.ok;

			StartCoroutine (RotateSouth (rotatingSpeed, allowed));		
		}
	}

	void MoveNorth(){
		MovementResponse response = gridController.MovePlayer (Direction.North);
		if(response != MovementResponse.blocked){
			bool allowed = response == MovementResponse.ok;

			StartCoroutine (RotateNorth (rotatingSpeed, allowed));		
		}
	}

	void MoveWest(){
		MovementResponse response = gridController.MovePlayer (Direction.West);
		if(response != MovementResponse.blocked){
			bool allowed = response == MovementResponse.ok;
		
			StartCoroutine (RotateWest (rotatingSpeed, allowed));		
		}
	}

	void MoveEast(){
		MovementResponse response = gridController.MovePlayer (Direction.East);
		if(response != MovementResponse.blocked){
			bool allowed = response == MovementResponse.ok;

			StartCoroutine (RotateEast (rotatingSpeed, allowed));		
		}
	}
		
	/// <summary>
	/// Rotates gameObject towards the south.
	/// </summary>
	/// <returns>The south.</returns>
	/// <param name="Rotation speed">Speed.</param>

	IEnumerator RotateSouth(float speed, bool allowed){
		moving = true;
		Vector3 initPos = transform.position;
		Quaternion initRot = transform.rotation;

		Vector3 pivot =transform.position + Vector3.down / 2 + Vector3.back / 2;
		float degrees = 0f;
		PlayRandomStripOfAudio();
		while (degrees < 90f) {
			float step = speed * Time.deltaTime;
			if (degrees + step > 90)
				step = 90 - degrees;
			
			transform.RotateAround (pivot, Vector3.left, step);
			degrees += step;
			yield return new WaitForEndOfFrame ();
		}

		transform.position = initPos + Vector3.back;
		//transform.rotation = initRot;
		//transform.Rotate(new Vector3(-90f,0f,0f));
		moving = false;
		audioSource.Stop ();
		if(!allowed)
			StartCoroutine(RotateNorth(rotatingSpeed * 3, true));

	}

	/// <summary>
	/// Rotates towards the north.
	/// </summary>
	/// <returns>The north.</returns>
	/// <param name="Rotation Speed">Speed.</param>
	IEnumerator RotateNorth(float speed, bool allowed){
		moving = true;
		Vector3 initPos = transform.position;
		Quaternion initRot = transform.rotation;

		Vector3 pivot =transform.position + Vector3.down / 2 + Vector3.forward / 2;
		float degrees = 0f;
		PlayRandomStripOfAudio();
		while (degrees < 90f) {
			float step = speed * Time.deltaTime;
			if (degrees + step > 90)
				step = 90 - degrees;

			transform.RotateAround (pivot, Vector3.right, step);
			degrees += step;
			yield return new WaitForEndOfFrame ();
		}

		transform.position = initPos + Vector3.forward;
		//transform.rotation = initRot;
		Vector3 rot = new Vector3 (initRot.x,initRot.y,initRot.z);

		rot += (Vector3.right * 90f);

		//transform.rotation = initRot * Quaternion.Euler(new Vector3(90f,0f,0f));
		//transform.rotation = Quaternion.Euler(rot);
		moving = false;
		audioSource.Stop ();
		if(!allowed)
			StartCoroutine(RotateSouth(rotatingSpeed * 3, true));
	}

	/// <summary>
	/// Rotates towards the east.
	/// </summary>
	/// <returns>The east.</returns>
	/// <param name="speed">Rotation speed.</param>
	IEnumerator RotateEast(float speed, bool allowed){
		moving = true;
		Vector3 initPos = transform.position;
		Quaternion initRot = transform.rotation;

		Vector3 pivot = transform.position + Vector3.down / 2 + Vector3.right / 2;
		float degrees = 0f;
		PlayRandomStripOfAudio();
		while (degrees < 90f) {
			float step = speed * Time.deltaTime;
			if (degrees + step > 90)
				step = 90 - degrees;

			transform.RotateAround (pivot, Vector3.back, step);
			degrees += step;
			yield return new WaitForEndOfFrame ();
		}

		transform.position = initPos + Vector3.right;
		//transform.rotation = initRot;
		//transform.Rotate(new Vector3(0f,0f,-90f));
		moving = false;
		audioSource.Stop ();
		if(!allowed)
			StartCoroutine(RotateWest(rotatingSpeed * 3, true));

	}

	/// <summary>
	/// Rotates towards the west.
	/// </summary>
	/// <returns>The west.</returns>
	/// <param name="speed">rotation speed.</param>
	IEnumerator RotateWest(float speed, bool allowed){
		moving = true;
		Vector3 initPos = transform.position;
		Quaternion initRot = transform.rotation;

		Vector3 pivot = transform.position + Vector3.down / 2 + Vector3.left / 2;
		float degrees = 0f;
		PlayRandomStripOfAudio ();
		while (degrees < 90f) {
			float step = speed * Time.deltaTime;

			if (degrees + step > 90)
				step = 90 - degrees;

			transform.RotateAround (pivot, Vector3.forward, step);
			degrees += step;

			yield return new WaitForEndOfFrame ();
		}

		transform.position = initPos + Vector3.left;
		//transform.rotation = initRot;
		//transform.Rotate(new Vector3(0f,0f,90f));
		moving = false;
		audioSource.Stop ();
		if(!allowed)
			StartCoroutine(RotateEast(rotatingSpeed * 3, true));

	}

	void PlayRandomStripOfAudio(){
		float lastBeggining = audioSource.clip.length - 3f;
		audioSource.pitch = pitches [current_note];
		if (current_note < pitches.Length - 1)
			current_note++;
		else
			current_note = 0;
		audioSource.Play();
	}
	#if UNITY_EDITOR
	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (transform.position + Vector3.down/2 + Vector3.back/2, .1f);
	}
	#endif
}

public class _myclass{
	double a = (double)'C';
}