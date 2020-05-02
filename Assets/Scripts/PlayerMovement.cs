/** Jonathan So, jonso.gamedev@gmail.com
 * Handles inputs for moving the player and remaining in bounds.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// PRIVATE
	private Vector2 startPos;
	private Vector2 dir; // The direction we're moving in.
	private float speed = 8f;
	private int xMove, yMove; // Ints that are -1, 0, or 1, used to build the dir vector above.

	// CONSTANT
	private const float X_BOUND = 4.5f;
	private const float Y_BOUND = 6.5f;

	// COMPONENT
	private Transform tm;

	// Get all the components we need.
	private void Awake() {
		tm = GetComponent<Transform>();
		startPos = tm.position;
	}

	private void Update() {
		// Build the dir vector
		if (Input.GetKey(KeyCode.W)) {
			yMove = 1;
		} else if (Input.GetKey(KeyCode.S)) {
			yMove = -1;
		} else {
			yMove = 0;
		}
		if (Input.GetKey(KeyCode.D)) {
			xMove = 1;
		} else if (Input.GetKey(KeyCode.A)) {
			xMove = -1;
		} else {
			xMove = 0;
		}
		// Apply the dir vector
		dir = new Vector2(xMove, yMove).normalized;
		tm.Translate(speed * dir * Time.deltaTime);

		// Bounds Checking
		if (tm.position.x < -X_BOUND) { // Left
			tm.position = new Vector2(-X_BOUND, tm.position.y);
		} else if (tm.position.x > X_BOUND) { // Right
			tm.position = new Vector2(X_BOUND, tm.position.y);
		}
		if (tm.position.y < -Y_BOUND) { // Bottom
			tm.position = new Vector2(tm.position.x, -Y_BOUND);
		} else if (tm.position.y > Y_BOUND) { // Top
			tm.position = new Vector2(tm.position.x, Y_BOUND);
		}
	}
}
