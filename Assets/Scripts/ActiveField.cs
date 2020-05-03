/** Jonathan So, jonso.gamedev@gmail.com
 * The active field object keeps all the action (bullets, enemies) within the playing field. 
 * When enemy spawner objects touch it, that activates its enemy (spawn dependent on threat level).
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveField : MonoBehaviour {

	// Despawns bullets when they leave the bounds of the screen.
	// Deactivates enemy powers (like shooting) when they leave the screen.
	private void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("PlayerBullet") || coll.gameObject.tag.Equals("EnemyBullet")) {
			coll.gameObject.SetActive(false);
		}
		if (coll.gameObject.tag.Equals("GroundEnemy") || coll.gameObject.tag.Equals("AirEnemy")) {
			Exploder exp = coll.gameObject.GetComponent<Exploder>();
			EnemyAim nma = coll.gameObject.GetComponent<EnemyAim>();
			Tank tnk = coll.gameObject.GetComponent<Tank>();
			Cannon can = coll.gameObject.GetComponent<Cannon>();
			if (exp) { exp.StopShoot(); }
			if (nma) { nma.StopShoot(); }
			if (tnk) { tnk.StopShoot(); }
			if (can) { can.StopShoot(); }
			// If an enemy was left alive, decrease the threat level.
			ThreatLevel.instance.AddKillLevel(-1f);
		}
	}
}
