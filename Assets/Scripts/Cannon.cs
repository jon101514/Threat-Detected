/** Jonathan So, jonso.gamedev@gmail.com
 * A cannon fires its payload forward after waiting a set amount of time.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

	// PUBLIC
	public int shotType;
	public float shotTimer; // Wait this amount of time, then fire a shot, and repeat.

	// PRIVATE
	private bool canShoot;

	// COMPONENT
	private Transform tm;

	private void Awake() {
		tm = GetComponent<Transform>();
	}

	private void OnEnable() {
		canShoot = true;
		StartCoroutine(IntervalShoot());
	}

	// At a regular interval (shotTimer), shoot a bullet.
	private IEnumerator IntervalShoot() {
		while (canShoot) {
			yield return new WaitForSeconds(shotTimer);
			if (canShoot) { EnemyBulletPool.instance.FireFromPool(shotType, tm, -Vector2.up); } 
		}
	}

	public void StopShoot() {
		canShoot = false;
	}
}
