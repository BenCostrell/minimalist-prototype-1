using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalController : MonoBehaviour {

	public int colorNum;
	public List<Color> colorList;
	public int score;

	// Use this for initialization
	void Awake () {
		InitializeColorList ();
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetColor(int cNum){
		GetComponent<SpriteRenderer> ().color = colorList[cNum];
		colorNum = cNum;
	}

	void InitializeColorList(){
		colorList = new List<Color> ();
		colorList.Add (Color.white);
		colorList.Add (Color.red);
		colorList.Add (Color.blue);
		colorList.Add (Color.magenta);
	}
}
