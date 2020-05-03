/** Jonathan So, jonso.gamedev@gmail.com
 * Based on OctoManGames' "Not Galaga" tutorial. Spawns waves of enemies who follow paths at time intervals.
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupSpawner : MonoBehaviour {

	public List<GameObject> spawnTypes; // The public list of all the different enemies we spawn in. Note that this is now dependent on threat level (0 is easy, 1 is med, 2 is hard)
	public List<SpawnWave> spawnWaves;
	public List<Path> paths; // List of all the paths we're using.

	// The definition for each wave we spawn in, including:
	// Time to wait
	// Spawn Position
	// List of enemies to spawn in
	// Time in between each spawn
	[System.Serializable]
	public class SpawnWave { 
		public float waitTime;
		public int spawnPos; // as an index of spawnPositions, look up there
		public int[] enemyList; // array of enemy types, as in spawnTypes
		public float betweenTime;
	}

	private void Awake() {
		StartCoroutine(SpawnEnemyWaves());
	}

	private IEnumerator SpawnEnemyWaves() {
		float runningTime = 0f;
		for (int i = 0; i < spawnWaves.Count; i++) {
			yield return new WaitForSeconds(spawnWaves[i].waitTime - runningTime);
			Debug.Log("WAVE: " + i + " -- WAITED " + (spawnWaves[i].waitTime - runningTime) + " SECONDS");
			for (int j = 0; j < spawnWaves[i].enemyList.Length; j++) {				
				float threat = ThreatLevel.instance.GetThreatPercent();
				int threatIndex = 0;
				if (threat < 1/3f) {
					threatIndex = 0;
				} else if (threat < 2/3f) {
					threatIndex = 1;
				} else {
					threatIndex = 2;
				}
				GameObject obj = Instantiate(spawnTypes[threatIndex], paths[spawnWaves[i].spawnPos].transform.GetChild(0).position, Quaternion.identity);
				obj.GetComponent<EnemyPathMovement>().pathToFollow = paths[spawnWaves[i].spawnPos];
				yield return new WaitForSeconds(spawnWaves[i].betweenTime);
			}
			runningTime = spawnWaves[i].waitTime;
		}
	}
}
