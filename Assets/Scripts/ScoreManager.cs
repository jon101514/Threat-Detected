/** Jonathan So, jonso.gamedev@gmail.com
 * Singleton that manages keeping, displaying, and saving the score.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	// SINGLETON
	public static ScoreManager instance;

	// PUBLIC

	// PRIVATE
	private int score;
	private int pointsToNextLife = 100000; // Every 100k, give the player another life.
	private string score2Str; // Performing toString on score

	// CONSTANT
	private const int NUM_PLACES = 12; // Can have a score of 999 999 999 999
	private const int EVERY_EXTEND = 100000; // Corresponds to pointsToNextLife.

	// COMPONENT
	private Text scoreText; // Attached to this text.

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		scoreText = GetComponent<Text>();
		score = 0;
		UpdateUIScore();
	}

	public void AddScore(int addToScore) {
		int add = (int) (addToScore * ThreatLevel.instance.GetMultiplier()); 
		score += add;
		ExtendCheck(add); // See if the player can get an extra life.
		UpdateUIScore();
	}

	private void ExtendCheck(int pts) {
		pointsToNextLife -= pts;
		if (pointsToNextLife <= 0) { // The player has earned enough to get an extra life.
			Debug.Log("EXTEND!");
			pointsToNextLife = Mathf.Abs(EVERY_EXTEND - (pointsToNextLife % EVERY_EXTEND));
			PlayerSpawner.instance.AddLives(1);
		}
	}

	public void SaveScore() {
		PlayerPrefs.SetInt("score", score);
		if (PlayerPrefs.GetInt("hiscore") < score || 25000 < score) {
			PlayerPrefs.SetInt("hiscore", score);
		} else {
			PlayerPrefs.SetInt("hiscore", 25000);
		}
	}

	// Update the score in the UI.
	private void UpdateUIScore() {
		// Make sure the score occupies exactly 12 decimal places
		scoreText.text = "1P ";
		score2Str = score.ToString();
		for (int i = 0; i < NUM_PLACES - score2Str.Length; i++) {
			scoreText.text += " ";
		}
		scoreText.text += score.ToString();
	}
}
