/** Jonathan So, jonso.gamedev@gmail.com
 * Temporary; ends the level after the player has lost all their lives or it's over (level lasts 84 seconds)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempLevelEnder : MonoBehaviour {

	public static TempLevelEnder instance;

	private const float LEVEL_TIME = 84f;

	private void Awake() {
		if (instance == null ) { instance = this; }
	}

	private void Start() {
		Invoke("EndLevel", LEVEL_TIME);
	}

	public void EndLevel() {
		ScoreManager.instance.SaveScore();
		SceneManager.LoadScene("Game Over");
	}
}
