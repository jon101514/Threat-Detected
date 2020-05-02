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

	// COMPONENT
	private Transform tm;

	private void Awake() {
		tm = GetComponent<Transform>();
	}

	private void OnEnable() {
		StartCoroutine(IntervalShoot());
	}

	// At a regular interval (shotTimer), shoot a bullet.
	private IEnumerator IntervalShoot() {
		while (true) {
			yield return new WaitForSeconds(shotTimer);
			EnemyBulletPool.instance.FireFromPool(shotType, tm, -Vector2.up);
		}
	}
}
