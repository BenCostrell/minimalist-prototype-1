using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public CircleController circController;

	// Use this for initialization
	void Start () {
		InitializeColor ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Reset")){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		Move ();
	}

	void InitializeColor(){
		circController = GetComponent<CircleController> ();
		circController.SetColor (0);
	}


	void Move(){
		Vector2 direction = Vector2.zero;
		if (Input.GetAxis("Horizontal") > 0){
			direction += Vector2.right;
		}
		if (Input.GetAxis ("Horizontal") < 0){
			direction += Vector2.left;
		}
		if (Input.GetAxis("Vertical") > 0){
			direction += Vector2.up;
		}
		if (Input.GetAxis ("Vertical") < 0){
			direction += Vector2.down;
		}
		GetComponent<Rigidbody2D> ().velocity = speed * direction.normalized;
	}
}
