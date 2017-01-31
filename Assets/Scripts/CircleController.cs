using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleController : MonoBehaviour {

	public int colorNum;
	private List<Color> colorList;
	public int colorNumPriorFrame;
	private GameObject scoreManager;


	// Use this for initialization
	void Awake () {
		InitializeColorList ();
		scoreManager = GameObject.FindWithTag ("ScoreManager");
	}
	
	// Update is called once per frame
	void Update () {
		colorNumPriorFrame = colorNum;
	}

	void InitializeColorList(){
		colorList = new List<Color> ();
		colorList.Add (Color.white);
		colorList.Add (Color.red);
		colorList.Add (Color.blue);
		colorList.Add (Color.magenta);
	}

	public void SetColor(int cNum){
		GetComponent<SpriteRenderer> ().color = colorList[cNum];
		colorNum = cNum;
	}

	void AssignNewColor(int collidedCircleColorNum){
		if (colorNum != collidedCircleColorNum) {
			int newCNum = colorNum + collidedCircleColorNum;
			if (newCNum > 3) {
				newCNum = 0;
			}
			SetColor (newCNum);
		}
	
	}


	void OnCollisionEnter2D(Collision2D collision){
		GameObject collidedObject = collision.gameObject;
		if ((collidedObject.tag == "circle") || (collidedObject.tag == "Player")) {
			AssignNewColor (collidedObject.GetComponent<CircleController> ().colorNumPriorFrame);
		} else if (collidedObject.tag == "goal") {
			GoalController goalCont = collidedObject.GetComponent<GoalController> ();
			if ((goalCont.colorNum == colorNum) && (gameObject.tag != "Player")) {
				Destroy (gameObject);
				scoreManager.GetComponent<ScoreManager> ().Score (goalCont.colorNum);
			} else {
				AssignNewColor (goalCont.colorNum);
			}
		}
	}
}
