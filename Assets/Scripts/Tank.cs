/** Jonathan So, jonso.gamedev@gmail.com
 * The tank enemy (Box, Star Box, and Ball) which moves in a direction and shoots an aimed bullet.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {

	// PUBLIC
	public Vector2 dir; // Recommended to slowly move down-left or down-right diagonally. Will normalize on Awake.
	public int shotType;
	public float speed;
	public float shotTimer; // Wait this amount of time, then fire a shot, and repeat.

	// PRIVATE
	private bool canShoot;

	// COMPONENT
	private EnemyAim nmeAim;
	private Transform tm;

	private void Awake() {
		nmeAim = GetComponent<EnemyAim>();
		tm = GetComponent<Transform>();
		dir.Normalize();
	}

	private void OnEnable() {
		canShoot = true;
		StartCoroutine(AimAndShoot());
	}

	private void Update() {
		tm.Translate(dir * speed * Time.deltaTime);
	}

	// At a regular interval (shotTimer), shoot an aimed bullet using our EnemyAim component.
	private IEnumerator AimAndShoot() {
		while (canShoot) {
			yield return new WaitForSeconds(shotTimer);
			if (canShoot) { nmeAim.Shoot(shotType); }
		}
	}

	public void StopShoot() {
		canShoot = false;
	}
}
