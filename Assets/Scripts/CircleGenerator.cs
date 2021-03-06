﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleGenerator : MonoBehaviour {

	public GameObject circlePrefab;
	public GameObject blueGoal1;
	public GameObject blueGoal2;
	public GameObject redGoal1;
	public GameObject redGoal2;
	public GameObject playerPrefab;

	public int numInitialCircles;
	public float minAcceptableDistance;
	public float leftBoundary;
	public float rightBoundary;
	public float topBoundary;
	public float bottomBoundary;

	private List<GameObject> circleList;
	private GameObject player1;
	private GameObject player2;

	private List<int> circleColorBag;

	public float winPulseMagnitude;
	public float winPulseDuration;
	public float finalWinScale;
	public float finalWinDuration;

	// Use this for initialization
	void Start () {
		GenerateInitialSetup ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GenerateInitialSetup() {
		circleList = new List<GameObject> ();
		circleColorBag = new List<int> ();

		FillBag ();

		InitializePlayers ();
		InitializeGoals ();

		for (int i = 0; i < numInitialCircles; i++) {
			Vector2 location = GenerateRandomLocation ();
			GameObject circle = Instantiate (circlePrefab, new Vector3 (location.x, location.y, 0), Quaternion.identity) as GameObject;
			bool isValidated = ValidateLocation (circle);
			while (!isValidated) {
				Destroy (circle);
				location = GenerateRandomLocation ();
				circle = Instantiate (circlePrefab, new Vector3 (location.x, location.y, 0), Quaternion.identity) as GameObject;
				isValidated = ValidateLocation (circle);
			}
			CreateCircle (circle);
		}
	}


	void FillBag(){
		for (int i = 0; i < 10; i++) {
			circleColorBag.Add (1);
			circleColorBag.Add (2);
		}
	}

	void InitializePlayers(){
		player1 = Instantiate (playerPrefab, new Vector3 (-5, 0, 0), Quaternion.identity) as GameObject;
		player2 = Instantiate (playerPrefab, new Vector3 (5, 0, 0), Quaternion.identity) as GameObject;

		player1.GetComponent<PlayerController> ().playerNum = 1;
		player2.GetComponent<PlayerController> ().playerNum = 2;

		player1.GetComponent<PlayerController> ().isActive = true;
		player2.GetComponent<PlayerController> ().isActive = true;


		circleList.Add (player1);
		circleList.Add (player2);
	}

	void InitializeGoals(){
		GoalController redGoal1Cont = redGoal1.GetComponent<GoalController> ();
		GoalController blueGoal1Cont = blueGoal1.GetComponent<GoalController> ();
		GoalController redGoal2Cont = redGoal2.GetComponent<GoalController> ();
		GoalController blueGoal2Cont = blueGoal2.GetComponent<GoalController> ();

		redGoal1Cont.SetColor (1);
		blueGoal1Cont.SetColor (2);
		redGoal2Cont.SetColor (1);
		blueGoal2Cont.SetColor (2);

		circleList.Add (redGoal1);
		circleList.Add (redGoal2);
		circleList.Add (blueGoal1);
		circleList.Add (blueGoal2);

		foreach (GameObject go in circleList) {
			if (go.tag == "goal") {
				GoalController goalCont = go.GetComponent<GoalController> ();
				goalCont.winPulseMagnitude = winPulseMagnitude;
				goalCont.winPulseDuration = winPulseDuration;
				goalCont.finalWinScale = finalWinScale;
				goalCont.finalWinDuration = finalWinDuration;
			}
		}

	}

	void CreateCircle(GameObject circle){
		circleList.Add (circle);
		CircleController cirCont = circle.GetComponent<CircleController> ();
		cirCont.SetColor (ChooseRandomColor());
	}

	Vector2 GenerateRandomLocation(){
		float x = Random.Range (leftBoundary, rightBoundary);
		float y = Random.Range (bottomBoundary, topBoundary);
		return new Vector2 (x, y);
	}

	bool ValidateLocation(GameObject circle){
		bool isValidated = true;
		CircleCollider2D circleCollider = circle.GetComponent<CircleCollider2D> ();
		if (circleList.Count > 0) {
			foreach (GameObject circ in circleList) {
				float minDist = minAcceptableDistance;
				if (circ.tag == "goal") {
					minDist += 3;
				}
				if (Vector3.Distance(circle.transform.position, circ.transform.position) < minDist) {
					isValidated = false;
					break;
				}
			}
		}
		return isValidated;
	}



	int ChooseRandomColor(){
		int colorIndex = Random.Range (0, circleColorBag.Count);
		int colorNum = circleColorBag [colorIndex];
		circleColorBag.Remove (colorNum);
		return colorNum;
	}

}
