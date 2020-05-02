/** Jonathan So, jonso.gamedev@gmail.com
 * Singleton; a communal object pool for the enemy force's bullets. Handles multiple bullet types.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour {

	// SINGLETON
	public static EnemyBulletPool instance;

	// PUBLIC
	public List<GameObject> bulletTypes; // Prefabs of the bullet types.
	public List<int> bulletClipSizes;

	// PRIVATE
	private List<GameObject> clipA, clipB, clipC, clipD; // Clips for the bullet types we handle.

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	private void OnEnable() {
		SetupObjectPool();
	}

	private void SetupObjectPool() {
		clipA = new List<GameObject>();
		clipB = new List<GameObject>();
		clipC = new List<GameObject>();
		clipD = new List<GameObject>();
		for (int i = 0; i < bulletClipSizes[0]; i++) {
			GameObject bull = (GameObject) Instantiate (bulletTypes[0]);
			bull.SetActive(false);
			clipA.Add(bull);
		}
		for (int i = 0; i < bulletClipSizes[1]; i++) {
			GameObject bull = (GameObject) Instantiate (bulletTypes[1]);
			bull.SetActive(false);
			clipB.Add(bull);
		}
		for (int i = 0; i < bulletClipSizes[2]; i++) {
			GameObject bull = (GameObject) Instantiate (bulletTypes[2]);
			bull.SetActive(false);
			clipC.Add(bull);
		}
		for (int i = 0; i < bulletClipSizes[3]; i++) {
			GameObject bull = (GameObject) Instantiate (bulletTypes[3]);
			bull.SetActive(false);
			clipD.Add(bull);
		}
	}

	/** Given a type, transform, and direction vector, fire a certain type of bullet. 
	 * param[type] - an int, 0, 1, 2, or 3, specifying the bullet type (corresponding to the four clip types).
	 * param[tm] - the transform of the object we want to fire from. Influences position and rotation.
	 * param[pDir] - the direction vector we want the bullet to have.
	 */
	public void FireFromPool(int type, Transform tm, Vector2 pDir) {
		List<GameObject> clip;
		switch(type) {
			case 0:
				clip = clipA;
				break;
			case 1:
				clip = clipB;
				break;
			case 2:
				clip = clipC;
				break;
			case 3:
			default:
				clip = clipC;
				break;
		}
		foreach (GameObject bull in clip) {
			if (!bull.activeInHierarchy) {
				bull.SetActive(true);
				bull.transform.position = tm.position;
				bull.transform.rotation = tm.rotation;
				bull.GetComponent<Bullet>().dir = pDir;
				break;
			}
		}
	}
}
