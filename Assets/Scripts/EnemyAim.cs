/** Jonathan So, jonso.gamedev@gmail.com
 * Component which tracks the player's position for enemies that shoot aimed bullets.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour {

	// PUBLIC

	// PRIVATE
	private Vector2 oldPlayerPos; // The position of the player one second into the past.
	private Vector2 newPlayerPos; // The position of the player right now.

	// CONSTANT

	// COMPONENT

	// A public coroutine-starter for WaitAndShoot.
	public void Shoot(int type) {
		StartCoroutine(WaitAndShoot(type));
	}

	// Calculate a vector from our position to the position of the player, then send it off to the EnemyBulletPool to shoot.
	private IEnumerator WaitAndShoot(int type) {
		oldPlayerPos = FindObjectOfType<PlayerMovement>().transform.position;
		// Wait one second
		yield return new WaitForSeconds(1f - Time.deltaTime);
		newPlayerPos = FindObjectOfType<PlayerMovement>().transform.position;
		Vector2 aimVector = (Vector2.Lerp(oldPlayerPos, newPlayerPos, Random.Range(0f, 1f)) - (Vector2) transform.position).normalized; 
		Debug.Log("RANDOMIZED LERP");
		EnemyBulletPool.instance.FireFromPool(type, transform, aimVector);
	}

	// Public coroutine-starter for LogData.
	public void StartLoggingData() {
		StartCoroutine(LogData());
	}

	// Logs the old player position, waits one second (minus a frame), then gets the new player position.
	private IEnumerator LogData() {
		oldPlayerPos = FindObjectOfType<PlayerMovement>().transform.position;
		// Wait one second
		yield return new WaitForSeconds(1f - Time.deltaTime);
		newPlayerPos = FindObjectOfType<PlayerMovement>().transform.position;
	}

	// Returns the position of the player.
	public Vector2 GetPlayerPos() {
		Debug.Log("RANDOMIZED LERP");
		return Vector2.Lerp(oldPlayerPos, newPlayerPos, Random.Range(0f, 1f));
	}
}
