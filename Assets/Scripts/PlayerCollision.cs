/** Jonathan So, jonso.gamedev@gmail.com
 * Handles the player's collision with harmful objects, and talks to the relevant managers to tell them that the player touched something harmful.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	private bool invulnerable = false;

	private SpriteRenderer sr;

	private const float FLASH_TIME = 1/12f; // Flash this many times a second.
	private const int INVULNERABILITY = 28; // Amount of cycles to flash.

	private void Awake() {
		sr = GetComponent<SpriteRenderer>();
	}

	private void OnEnable() {
		StartCoroutine(Invulnerability());
	}

	// An invulnerability coroutine; while the player flashes, they are invincible. Lasts FLASH_TIME * INVULNERABILITY seconds.
	private IEnumerator Invulnerability() {
		invulnerable = true;
		for (int i = 0; i < INVULNERABILITY; i++) {
			sr.enabled = !sr.enabled;
			yield return new WaitForSeconds(FLASH_TIME);
		}
		invulnerable = false;
	}

	private void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag.Equals("EnemyBullet") || coll.gameObject.tag.Equals("GroundEnemy") || coll.gameObject.tag.Equals("AirEnemy")) {
			if (invulnerable) {
				return;
			} else {
				PlayerSpawner.instance.AddLives(-1);
				ThreatLevel.instance.AddLifeLevel(-64f); // Reset the life level parameter.
				PlayerSpawner.instance.Respawn();
				SFXManager.instance.PlayClip("destroy");
				gameObject.SetActive(false);
			}
		}
	}
}
