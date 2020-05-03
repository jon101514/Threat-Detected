/** Jonathan So, jonso.gamedev@gmail.com
 * Grabs references to all scrolling objects and makes them scroll vertically.
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingManager : MonoBehaviour {

	// PUBLIC
	public List<TimeAndSpeed> timeAndSpeed;

	// PRIVATE
	private float speed;
	private List<Scrolling> scrollObjects;

	// Object to hold two floats
	[System.Serializable]
	public class TimeAndSpeed {
		public float timeMark;
		public float speedAtTime;
	}

	private void Start() {
		StartCoroutine(ScrollSpeed());
	}

	// Manages the speed parameter according to the times in timeAndSpeed.
	private IEnumerator ScrollSpeed() {
		for (int i = 0; i < timeAndSpeed.Count; i++) {
			speed = timeAndSpeed[i].speedAtTime;
			scrollObjects = new List<Scrolling>(FindObjectsOfType<Scrolling>());
			foreach (Scrolling obj in scrollObjects) {
				obj.SetSpeed(speed);
			}
			yield return new WaitForSecondsRealtime(timeAndSpeed[i].timeMark);
		}
	}
}
