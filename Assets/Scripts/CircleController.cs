using UnityEngine;
using System.Collections;

public class CircleController : MonoBehaviour {

	public Color circleColor;

	// Use this for initialization
	void Start () {
		SetColor (circleColor);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetColor(Color colorToSet){
		GetComponent<SpriteRenderer> ().color = colorToSet;
		circleColor = colorToSet;
	}
}
