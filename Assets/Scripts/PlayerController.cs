using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float moveForce;
	public float maxSpeed;
	public CircleController circController;

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

	void InitializeColor(){
		circController = GetComponent<CircleController> ();
		circController.SetColor (0);
	}


	void Move(){
		Vector2 direction = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		direction = direction.normalized;

		if (Vector2.Dot(rb.velocity, direction) < maxSpeed) {
			rb.AddForce (moveForce * direction);
		} else {
			rb.velocity = maxSpeed * direction;
		}
	}
}
