using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {

	// PRIVATE
	[SerializeField]
	private float speed;

	// COMPONENT
	private Transform tm;

	private void Awake() {
		tm = GetComponent<Transform>();
	}

	private void Update() {
		tm.Translate(Vector2.down * speed * Time.deltaTime);
	}

	public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}
}
