/** Jonathan So, jonso.gamedev@gmail.com
 * Handles spawning in the player if they have enough lives, and loading the Game Over scene if they don't.
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour {

	// SINGLETON
	public static PlayerSpawner instance;

	// PUBLIC
	public PlayerCollision player;
	public Vector3 origin = new Vector3(0f, -4f, 0f);
	[Range (1f, 8f)]
	public float respawnTime;
	public GameObject lifeIcon; // Prefab for the life icon.
	public Transform lifeCounter; // The life counter in the Canvas. Parent the life icons to this.

	// PRIVATE
	private int lives = 2;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		SpawnPlayer();
		UpdateUI();
	}

	public void AddLives(int amount) {
		lives += amount;
		UpdateUI();
	}

	// Updates the life counter in the bottom-left corner.
	private void UpdateUI() {
		if (lifeCounter.childCount == lives) { // Don't do anything, since the life counter has the correct amount of lives.
			Debug.Log("="  + lifeCounter.childCount + "\t" + lives);
			return;
		} else if (lifeCounter.childCount < lives) { // Spawn life icons.
			Debug.Log("SPAWN "  + lifeCounter.childCount + "\t" + lives + ", so spawn " + (lives - lifeCounter.childCount));
			int spawnAmt = lives - lifeCounter.childCount;
			for (int i = 0; i < spawnAmt; i++) {
				GameObject icon = (GameObject) Instantiate(lifeIcon);
				icon.transform.SetParent(lifeCounter);
			}
		} else { // lifeCounter.childCount > lives; we only destroy one life at a time, so this removes one life icon.
			Debug.Log("DESTROY; " + lifeCounter.childCount + "\t" + lives);
			if (lifeCounter.childCount > 1) {
				Transform delete = lifeCounter.GetChild(0);
				delete.parent = null;
				Destroy(delete.gameObject);
			}
		}
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
	