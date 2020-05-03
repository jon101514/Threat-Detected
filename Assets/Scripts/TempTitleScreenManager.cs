/** Jonathan So, jonso.gamedev@gmail.com
 * Temporary, changes resolution for Windows build and lets the user start the game by pressing Enter.
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempTitleScreenManager : MonoBehaviour {

	private void Awake() {
		Screen.SetResolution(480, 640, false, 60); // Force 480x640 resolution.
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			SceneManager.LoadScene("Level");
		}	
	}
}
