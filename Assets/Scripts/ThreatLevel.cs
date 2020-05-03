/** Jonathan So, jonso.gamedev@gmail.com
 * Attached to a slider component, the threat level affects the game's difficulty, from 0 (easy) to 192 (hard).
 * Positive actions (firing, killing, staying alive, and --remaining powered up is UNUSED--) increase the threat level;
 * the opposite actions decrease the threat level.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For the slider

public class ThreatLevel : MonoBehaviour {

	// SINGLETON
	public static ThreatLevel instance;

	// PUBLIC
	public Text multiText;

	// PRIVATE
	private float threat; // The threat is a combination of the next four float values, all with a range of 0-64:
	private float fireLevel; // How often the player is firing bullets. Constantly decreases, and increases when the player fires.
	private float killLevel; // Number expressing how many enemies the player kills; this goes up with every kill and down with every spared enemy.
	private float lifeLevel; // A number that starts from zero and constantly ticks towards the maximum; resets only when the player dies.
	private float powLevel; // Number expressing how long the player has used powerups. Constantly ticks up with a rate dependent on the player's power level. UNUSED

	private float playerPow; // An expression of the player's power, from 0 (default weapons) to 1 (maximum weapons). UNUSED
	[SerializeField]
	private float multiplier; // Multiplier calculated based on threat level, from 0.5x to 8x.

	// CONSTANT
	private float MAX_LEVEL = 64f; // Maximum value for any of the "level" variables up there.
	private float MAX_THREAT = 192f; // Maximum value for the threat variable. 
	private float REPEAT_RATE = 1/2f; // Refresh the threat level and the UI at this interval.

	// COMPONENT
	private Slider slider;

	private void Awake() {
		if (instance == null) { instance = this; }
		slider = GetComponent<Slider>();
		threat = slider.value;
	}

	// Repeatedly call UpdateThreatLevel to update the threat level internally and in the UI. This is one "tick".
	private void OnEnable() {
		InvokeRepeating("UpdateThreatLevel", 0, REPEAT_RATE);
		// Debug.Log("TO DO: IMPLEMENT Life/Pow Levels;Life: reset on death, Pow: keeping track of player's current power level");
	}

	// Getter for the threat level.
	public float GetThreatLevel() { return threat; }

	// Getter for the threat level as a decimal (threat / MAX_THREAT).
	public float GetThreatPercent() { return (threat / MAX_THREAT); }

	// Getter for the threat-dependent multiplier.
	public float GetMultiplier() { return multiplier; }

	// Execute one "tick" for the four level variables and then apply those changes to the threat variable.
	private void UpdateThreatLevel() {
		// Execute a "tick"
		// AddFireLevel is called whenever the player fires, but it ticks down constantly when the player *doesn't* fire.
		AddFireLevel(-0.4f);
		// AddKillLevel is called for each kill and also for each spare. Not done in here.
		AddLifeLevel(1f);
		// AddPowLevel(playerPow); // UNUSED

		// threat = fireLevel + killLevel + lifeLevel + powLevel;
		threat = fireLevel + killLevel + lifeLevel;
		// Bounds-checking
		if (threat > MAX_THREAT) { threat = MAX_THREAT; }
		else if (threat < 0) { threat = 0; }

		CalculateMultiplier();
		slider.value = threat; // Display it in the UI after we're done making changes. 
	}

	// Calculates the point multiplier, which goes from 0.5 -> 8 times.
	private void CalculateMultiplier() {
		float threatPerc = GetThreatPercent();
		if (threatPerc < 1/2f) { // 0.5 to 1 range
			multiplier = Mathf.Lerp(0.5f, 1f, threatPerc / 0.5f);
		} else if (threatPerc < 3/4f) { // 1 to 2 range
			multiplier = Mathf.Lerp(1f, 2f, (threatPerc - 0.5f) / 0.25f);
		} else if (threatPerc < 7/8f) { // 2 to 4 range
			multiplier = Mathf.Lerp(2f, 4f, (threatPerc - 0.75f) / 0.125f);
		} else { // Maximum of 4 to 8 range
			multiplier = Mathf.Lerp(4f, 8f, (threatPerc - 0.875f) / 0.125f);
		}
		multiText.text = multiplier.ToString("0.00") + " x";
	}

	// Add to the FireLevel.
	public void AddFireLevel(float pFire) {
		fireLevel += pFire;
		// Bounds-checking
		if (fireLevel > MAX_LEVEL) { fireLevel = MAX_LEVEL; }
		else if (fireLevel < 0) { fireLevel = 0; }
	}

	// Add to the KillLevel.
	public void AddKillLevel(float pKill) {
		killLevel += pKill;
		// Bounds-checking
		if (killLevel > MAX_LEVEL) { killLevel = MAX_LEVEL; }
		else if (lifeLevel < 0) { killLevel = 0; }
	}

	// Add to the LifeLevel.
	public void AddLifeLevel(float pLife) {
		lifeLevel += pLife;
		// Bounds-checking
		if (lifeLevel > MAX_LEVEL) { lifeLevel = MAX_LEVEL; }
		else if (lifeLevel < 0) { lifeLevel = 0; }
	}

	// Add to the PowLevel.
	public void AddPowLevel(float pPow) {
		powLevel += pPow;
		// Bounds-checking
		if (powLevel > MAX_LEVEL) { powLevel = MAX_LEVEL; }
		else if (powLevel < 0) { powLevel = 0; }
	}

}
