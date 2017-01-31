﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalController : MonoBehaviour {

	public int colorNum;
	public List<Color> colorList;
	public int score;
	private GameObject tracker;
	private SpriteRenderer trackerRenderer;

	// Use this for initialization
	void Awake () {
		InitializeColorList ();
		score = 0;
		Transform[] children = GetComponentsInChildren<Transform> ();
		foreach (Transform child in children) {
			if (child.tag == "Tracker") {
				tracker = child.gameObject;
				trackerRenderer = tracker.GetComponent<SpriteRenderer> ();
				child.localScale = Vector3.zero;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetColor(int cNum){
		GetComponent<SpriteRenderer> ().color = colorList[cNum];
		trackerRenderer.color = colorList [cNum];
		colorNum = cNum;
	}

	void InitializeColorList(){
		colorList = new List<Color> ();
		colorList.Add (Color.white);
		colorList.Add (Color.red);
		colorList.Add (Color.blue);
		colorList.Add (Color.magenta);
	}

	public void Score(){
		tracker.transform.localScale += 0.09f * Vector3.one;
	}
}
