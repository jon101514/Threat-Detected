/** Jonathan So, jonso.gamedev@gmail.com
 * Handles object pools of explosions and hitsparks.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour {

	// SINGLETON
	public static FXManager instance;

	public GameObject hitsparkPrefab, explosionPrefab;

	private List<GameObject> hitsparks;
	private List<GameObject> explosions;
	private List<GameObject> grazesparks;

	private const int POOL_SIZE = 8;

	private void Awake () {
		if (instance == null) { instance = this; }
		SetupObjectPool();
	}

	private void SetupObjectPool() {
		hitsparks = new List<GameObject>();
		explosions = new List<GameObject>();
		grazesparks = new List<GameObject>();
		for (int i = 0; i < POOL_SIZE; i++) {
			GameObject hs = Instantiate(hitsparkPrefab);
			hs.SetActive(false);
			hitsparks.Add(hs);
			GameObject es = Instantiate(explosionPrefab);
			es.SetActive(false);
			explosions.Add(es);
		}
	}

	/* Displays an effect at a specified position with a selected scale.
	 * param[type] - the strings "hitspark" or "explosion"
	 * param[pos] - The position where we want to spawn the effect.
	 * param[scale] - how big we want the effect.
	 */
	public void ShowFX(string type, Vector2 pos, Vector3 scale) {
		List<GameObject> pool;
		switch (type) {
			case "explosion":
				pool = explosions;
				break;
			case "hitspark":
			default:
				pool = hitsparks;
				break;
		}
		for (int i = 0; i < pool.Count; i++) {
			GameObject curr = pool[i];
			if (!curr.gameObject.activeSelf) {
				curr.transform.position = pos;
				curr.transform.localScale = scale;
				curr.gameObject.SetActive(true);
				break;
			}
		}
	}
}
