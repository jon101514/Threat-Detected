/** Jonathan So, jonso.gamedev@gmail.com
 * Sets up object-pooled bullets for the player.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	// PUBLIC
	public GameObject bulletPrefab;
	public List<Transform> cannons; // Cannons are expressed by a transform to denote position and rotation.

	// PRIVATE
	private List<GameObject> clip;

	// CONSTANT
	private const int CLIP_SIZE = 64; // Size of the object pool.
	private const float FIRE_THREAT = 0.2f; // How much threat to add for each bullet fired.

	// COMPONENT
	private Transform tm;

	// Get our transform component.
	private void Awake() {
		tm = GetComponent<Transform>();
	}

	// Set up the object pool for our bullets.
	private void OnEnable() {
		SetupObjectPool();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.J)) {
			for (int i = 0; i < cannons.Count; i++) { // Fire all cannons.
				FireFromPool(cannons[i]);
				ThreatLevel.instance.AddFireLevel(FIRE_THREAT); // Add FIRE_THREAT for each bullet fired.
			}
		}
	}

	private void SetupObjectPool() {
		clip = new List<GameObject>();
		for (int i = 0; i < CLIP_SIZE; i++) {
			GameObject bull = (GameObject) Instantiate (bulletPrefab, tm.position, Quaternion.identity);
			bull.SetActive(false);
			clip.Add(bull);
		}
	}

	// Given a cannon's transform, fire a bullet from that cannon (with its position and rotation).
	// param[cannon] - the transform of the cannon we want to fire from.
	private void FireFromPool(Transform cannon) {
		foreach (GameObject bull in clip) {
			if (!bull.activeInHierarchy) {
				bull.SetActive(true);
				bull.transform.position = cannon.position;
				bull.transform.rotation = cannon.rotation;
				break;
			}
		}
	}
}
