using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleGenerator : MonoBehaviour {

	public GameObject circlePrefab;
	public int numInitialCircles;
	public float minAcceptableDistance;
	public float leftBoundary;
	public float rightBoundary;
	public float topBoundary;
	public float bottomBoundary;

	private List<Vector2> circleList;

	// Use this for initialization
	void Start () {
		GenerateInitialSetup ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GenerateInitialSetup() {
		circleList = new List<Vector2> ();
		circleList.Add (Vector2.zero);
		for (int i = 0; i < numInitialCircles; i++) {
			Vector2 location = GenerateRandomLocation ();
			bool isValidated = ValidateLocation (location);
			while (!isValidated) {
				location = GenerateRandomLocation ();
				isValidated = ValidateLocation (location);
			}
			GameObject circle = Instantiate (circlePrefab, new Vector3 (location.x, location.y, 0), Quaternion.identity) as GameObject;
			circleList.Add (location);
			circle.GetComponent<CircleController> ().SetColor (ChooseRandomColor());
		}
	}

	bool IsTooClose(Vector2 circleA, Vector2 circleB){
		if (Vector2.Distance (circleA, circleB) < minAcceptableDistance) {
			return true;
		} else {
			return false;
		}
	}

	Vector2 GenerateRandomLocation(){
		float x = Random.Range (leftBoundary, rightBoundary);
		float y = Random.Range (bottomBoundary, topBoundary);
		return new Vector2 (x, y);
	}

	bool ValidateLocation(Vector2 location){
		bool isValidated = true;
		if (circleList.Count > 0) {
			foreach (Vector2 circ in circleList) {
				if (IsTooClose (location, circ)) {
					isValidated = false;
					break;
				}
			}
		}
		return isValidated;
	}

	int ChooseRandomColor(){
		int colorIndex = Random.Range (1, 3);
		return colorIndex;
	}

}
