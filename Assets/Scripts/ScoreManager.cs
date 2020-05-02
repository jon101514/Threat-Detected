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
	private string score2Str; // Performing toString on score

	// CONSTANT
	private const int NUM_PLACES = 12; // Can have a score of 999 999 999 999

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
		score += addToScore;
		UpdateUIScore();
	}

	public void SaveScore() {
		PlayerPrefs.SetInt("score", score);
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
