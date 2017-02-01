using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

	public int redScore;
	public int blueScore;

	public GameObject redGoal1;
	public GameObject redGoal2;
	public GameObject blueGoal1;
	public GameObject blueGoal2;

	public bool gameWon;
	public bool finalScreenShown;

	// Use this for initialization
	void Start () {
		gameWon = false;
		finalScreenShown = false;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetKeyDown (KeyCode.Space)) {
			gameWon = true;
			GameWin (1);
		}*/
		if (Input.GetButtonDown("Reset")){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		if (Input.GetButtonDown("ToggleFullScreen")){
			Screen.fullScreen = !Screen.fullScreen;
		}
			
	}

	public void Score(int colorNum){
		if (colorNum == 1) {
			redScore += 1;
			if ((redScore >= 10) && !gameWon) {
				gameWon = true;
				GameWin (1);
			}
			redGoal1.GetComponent<GoalController> ().Score ();
			redGoal2.GetComponent<GoalController> ().Score ();
		}
		else if (colorNum == 2) {
			blueScore += 1;
			if ((blueScore >= 10) && !gameWon) {
				gameWon = true;
				GameWin (2);
			}
			blueGoal1.GetComponent<GoalController> ().Score ();
			blueGoal2.GetComponent<GoalController> ().Score ();
		}
	}

	void GameWin(int playerNum){
		GetComponent<AudioSource> ().Play ();
		if (playerNum == 1) {
			InitiateWinPulse (redGoal1);
			InitiateWinPulse (redGoal2);
		}
		else if (playerNum == 2) {
			InitiateWinPulse (blueGoal1);
			InitiateWinPulse (blueGoal2);
		}
	}

	void InitiateWinPulse(GameObject goal){
		goal.GetComponent<SpriteRenderer> ().sortingOrder = 3;
		goal.GetComponent<CircleCollider2D> ().enabled = false;
		goal.GetComponent<GoalController> ().WinPulse (2);
	}
}
