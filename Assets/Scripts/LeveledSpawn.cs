/** Jonathan So, jonso.gamedev@gmail.com
 * Spawns a level 1, 2, or 3 enemy depending on the Threat Level.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeveledSpawn : MonoBehaviour {

	// PUBLIC
	public GameObject lv1, lv2, lv3; // The level 1, 2, and 3 prefabs to spawn here.

	private void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("ActiveField")) {
			float threat = ThreatLevel.instance.GetThreatPercent();
			GameObject obj;
			if (threat < 1/3f) {
				Debug.Log("Spawn LV 1 " + threat);
				obj = (GameObject) Instantiate(lv1, transform.position, Quaternion.identity);
			} else if (threat < 2/3f) {
				Debug.Log("Spawn LV 2 " + threat);
				obj = (GameObject) Instantiate(lv2, transform.position, Quaternion.identity);
			} else {
				Debug.Log("Spawn LV 3 " + threat);
				obj = (GameObject) Instantiate(lv3, transform.position, Quaternion.identity);
			}
			obj.transform.parent = transform;
		}
	}

	private void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag.Equals("ActiveField")) {
			this.gameObject.SetActive(false);
		}
	}
}
