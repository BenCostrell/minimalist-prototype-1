﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalController : MonoBehaviour {

	public int colorNum;
	public List<Color> colorList;
	public int score;
	private GameObject tracker;
	private SpriteRenderer trackerRenderer;
	public float winPulseMagnitude;
	public float winPulseDuration;
	public float finalWinScale;
	public float finalWinDuration;
	private ScoreManager scoreManager;
	private CircleGenerator circleGenerator;

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
		scoreManager = GameObject.FindGameObjectWithTag ("ScoreManager").GetComponent<ScoreManager> ();
		circleGenerator = GameObject.FindGameObjectWithTag ("CircleGenerator").GetComponent<CircleGenerator> ();
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
		if (!scoreManager.gameWon) {
			GetComponent<AudioSource> ().Play ();
		}
	}

	public void WinPulse(int pulses){
		if (pulses > 0) {
			float pulseStrength = 4 + (winPulseMagnitude / pulses);
			iTween.ScaleTo (gameObject, iTween.Hash ("scale", pulseStrength * Vector3.one, "time", winPulseDuration, 
				"easetype", iTween.EaseType.easeOutBounce, "oncomplete", "WinPulse", "oncompleteparams", pulses - 1));
		} else {
			FinalWinTween ();
		}
	}

	void FinalWinTween(){
		iTween.ScaleTo (gameObject, iTween.Hash ("scale", finalWinScale * Vector3.one, "time", finalWinDuration, "oncomplete", "FinalWinScreen"));
	}

	void FinalWinScreen(){
		if (!scoreManager.finalScreenShown) {
			scoreManager.finalScreenShown = true;
			GameObject winScreen = Instantiate (circleGenerator.playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			winScreen.transform.localScale = 5 * Vector3.one;
			SpriteRenderer[] renderers = winScreen.GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer ren in renderers) {
				ren.sortingOrder += 5;
				if (ren.gameObject.tag == "Indicator") {
					ren.color = colorList [colorNum];
				}
			}
			winScreen.GetComponent<CircleCollider2D> ().enabled = false;
			winScreen.GetComponent<PlayerController> ().isActive = false;
		}
	}
}
