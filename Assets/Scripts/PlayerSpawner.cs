/** Jonathan So, jonso.gamedev@gmail.com
 * Handles spawning in the player if they have enough lives, and loading the Game Over scene if they don't.
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	// SINGLETON
	public static PlayerSpawner instance;

	// PUBLIC
	public PlayerCollision player;
	public Vector3 origin = new Vector3(0f, -4f, 0f);
	[Range (1f, 8f)]
	public float respawnTime;

	// PRIVATE
	private int lives = 2;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		SpawnPlayer();
	}

	public void AddLives(int amount) {
		lives += amount;
	}

	public void Respawn() {
		if (lives >= 0) {
			Invoke("SpawnPlayer", respawnTime);
		} else {
			TempLevelEnder.instance.EndLevel();
		}
	}

	private void SpawnPlayer() {
		player.transform.position = origin;
		player.gameObject.SetActive(true);
	}
}
	