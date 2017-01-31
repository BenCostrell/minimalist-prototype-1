using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public int redScore;
	public int blueScore;

	public GameObject redGoal1;
	public GameObject redGoal2;
	public GameObject blueGoal1;
	public GameObject blueGoal2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			GameWin (1);
		}
	}

	public void Score(int colorNum){
		if (colorNum == 1) {
			redScore += 1;
			redGoal1.GetComponent<GoalController> ().Score ();
			redGoal2.GetComponent<GoalController> ().Score ();
			if (redScore >= 10) {
				GameWin (1);
			}
		}
		else if (colorNum == 2) {
			blueScore += 1;
			blueGoal1.GetComponent<GoalController> ().Score ();
			blueGoal2.GetComponent<GoalController> ().Score ();
			if (blueScore >= 10) {
				GameWin (2);
			}
		}
	}

	void GameWin(int playerNum){
		if (playerNum == 1) {
			redGoal1.GetComponent<SpriteRenderer> ().sortingOrder = 3;
			redGoal2.GetComponent<SpriteRenderer> ().sortingOrder = 3;

			redGoal1.GetComponent<GoalController> ().WinPulse (2);
			redGoal2.GetComponent<GoalController> ().WinPulse (2);
		}
		else if (playerNum == 2) {
			blueGoal1.GetComponent<SpriteRenderer> ().sortingOrder = 3;
			blueGoal2.GetComponent<SpriteRenderer> ().sortingOrder = 3;

			blueGoal1.GetComponent<GoalController> ().WinPulse (2);
			blueGoal2.GetComponent<GoalController> ().WinPulse (2);
		}
	}
}
