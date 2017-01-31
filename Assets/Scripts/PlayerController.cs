using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float moveForce;
	public float maxSpeed;
	public CircleController circController;
	public int playerNum;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		InitializeColor ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Reset")){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void FixedUpdate(){
		Move ();
	}

	public void InitializeColor(){
		circController = GetComponent<CircleController> ();
		circController.SetColor (0);
		Transform[] children = GetComponentsInChildren<Transform> ();
		foreach (Transform child in children) {
			if (child.tag == "Indicator") {
				if (playerNum == 1) {
					child.GetComponent<SpriteRenderer> ().color = Color.red;
				} else if (playerNum == 2) {
					child.GetComponent<SpriteRenderer> ().color = Color.blue;
				}
			}
		}
	}


	void Move(){
		Vector2 direction = new Vector2 (Input.GetAxisRaw ("Horizontal_P" + playerNum), Input.GetAxisRaw ("Vertical_P" + playerNum));

		direction = direction.normalized;

		if (Vector2.Dot(rb.velocity, direction) < maxSpeed) {
			rb.AddForce (moveForce * direction);
		} else {
			rb.velocity = maxSpeed * direction;
		}
	}
}
