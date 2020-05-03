/** Jonathan So, jonso.gamedev@gmail.com
 * This script makes an object "scroll" if it's in the scene upon startup (so that ScrollingManager can pick it up).
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {

	// PRIVATE
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
