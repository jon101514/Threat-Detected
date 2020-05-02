/** Jonathan So, jonso.gamedev@gmail.com
 * Exploders fly towards the player's position then explode into 3, 5, or 7 bullets downward. They constantly rotate.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour {

	// PUBLIC
	public int numOfBullets; // The number of bullets to explode into.
	public int shotType; // Shot type corresponds to the EnemyBulletPool.
	public float speed;
	public float rotSpd;
	public float shotTimer; // Wait this amount of time, then, if not destroyed, explode into multiple bullets.

	// PRIVATE
	private Vector2 dir;
	private bool canMove = false;
	private bool canShoot = true;
	private float timeUntilExplode; // A constantly-ticking timer; when it hits zero, explode into multiple bullets.

	// COMPONENT
	private EnemyAim nmeAim; // Used not for firing, but for tracking the player's position.
	private Transform child; // Transform of the child object, which contains our sprite.
	private Transform tm;

	private void Awake() {
		nmeAim = GetComponent<EnemyAim>();
		child = transform.GetChild(0).transform;
		tm = GetComponent<Transform>();
	}

	// Upon being spawned, start making note of where the player is now and then one second later.
	private void OnEnable() {
		canMove = false;
		canShoot = true;
		nmeAim.StartLoggingData();
		Invoke("StartMoving", 1f);
		timeUntilExplode = shotTimer;
	}

	private void Update() {
		if (canMove) { // Only move when we have a vector to the player.
			tm.Translate(dir * speed * Time.deltaTime);
		}
		child.Rotate(0, 0, rotSpd * Time.deltaTime); // Sprite Rotation
		timeUntilExplode -= Time.deltaTime;
		if (timeUntilExplode <= 0 && canShoot) {
			Explode();
		}
	}

	// Destroy ourselves by exploding into multiple bullets (depending on threat level).
	private void Explode() {
		tm.Rotate(0, 0, -30f);
		for (int i = 0; i < numOfBullets; i++) {
			EnemyBulletPool.instance.FireFromPool(shotType, tm, -Vector2.up);
			tm.Rotate(0, 0, 120f/numOfBullets);
		}
		this.gameObject.SetActive(false);	
	}

	// Get the direction vector to the player, then start moving towards them.
	private void StartMoving() {
		dir = (nmeAim.GetPlayerPos() - (Vector2) tm.position).normalized;
		canMove = true;
	}

	public void StopShoot() {
		canShoot = false;
	}
}
