/** Jonathan So, jonso.gamedev@gmail.com
 * Handles the player's collision with harmful objects, and talks to the relevant managers to tell them that the player touched something harmful.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	private void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag.Equals("EnemyBullet") || coll.gameObject.tag.Equals("GroundEnemy") || coll.gameObject.tag.Equals("AirEnemy")) {
			PlayerSpawner.instance.AddLives(-1);
			ThreatLevel.instance.AddLifeLevel(-64f); // Reset the life level parameter.
			PlayerSpawner.instance.Respawn();
			gameObject.SetActive(false);
		}
	}
}
