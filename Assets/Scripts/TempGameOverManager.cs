/** Jonathan So, jonso.gamedev@gmail.com
 * Temporary; shows the game over text along with scores and boots back to the title screen.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TempGameOverManager : MonoBehaviour {

	public Text gameOverText;

	private void Start() {
		gameOverText.text = "GAME OVER\nYOUR SCORE: " + PlayerPrefs.GetInt("score") + "\nHIGH SCORE: " + PlayerPrefs.GetInt("hiscore");
		Invoke("BackToStart", 4f);
	}

	private void BackToStart() {
		SceneManager.LoadScene("Title Screen");
	}
}
