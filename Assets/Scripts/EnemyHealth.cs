/** Jonathan So, jonso.gamedev@gmail.com
 * Gives an enemy health, the ability to be destroyed, and will add points upon hit/destruction.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	// PUBLIC
	public int health;
	public int pointsOnHit; // How many points to grant when hit.
	public int pointsOnDestroy; // How many points to grant when destroyed.
	public Sprite destroySprite; // The sprite to display upon destruction.
	public bool hidden = false;

	// PRIVATE
	private List<Color> flashColors;
	private bool destroyed;

	// CONSTANT
	private const int FLASH_FRAMES = 8;

	// COMPONENT
	private Collider2D coll;
	private SpriteRenderer sr;

	private void Awake() {
		coll = GetComponent<Collider2D>();
		sr = GetComponent<SpriteRenderer>();
		if (!sr) { 
			sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
		}
		destroyed = false;
		SetupFlashPalette();
	}

	// Sets up the colors to go through when hit.
	private void SetupFlashPalette() {
		flashColors = new List<Color>();
		for (int i = 0; i < FLASH_FRAMES; i++) {
			flashColors.Add(Color.Lerp(Color.red, Color.white, (i / (float) FLASH_FRAMES)));
		}
	}

	// When hit, decrease health and add points, then check for destruction.
	private void Damage() {
		health--;
		ScoreManager.instance.AddScore(pointsOnHit);
		if (!hidden) { StartCoroutine(ColorFlash()); }
		DestroyCheck();
	}

	private IEnumerator ColorFlash() {
		for (int i = 0; i < FLASH_FRAMES; i++) {
			sr.color = flashColors[i];
			yield return new WaitForEndOfFrame();
		}
		sr.color = Color.white;
	}

	// If destroyed, then stop shooting, stop moving, and set the flags to indicate destruction.
	private void DestroyCheck() {
		if (health <= 0 && !destroyed) {
			destroyed = true;
			coll.enabled = false;
			sr.sprite = destroySprite;
			// Disable shooting.
			Exploder exp = GetComponent<Exploder>();
//			EnemyAimCannon eac = GetComponent<EnemyAimCannon>();
//			BackingUpMiniboss bumi = GetComponent<BackingUpMiniboss>();
			if (exp) { exp.StopShoot(); }
//			if (eac) { eac.StopShoot(); }
//			if (bumi) {bumi.StopShoot(); }

			// Disable movement.

//			EnemyPathMovement epm = GetComponent<EnemyPathMovement>();
//			if (epm) { epm.speed = 0; }

			// Add to score.
			ScoreManager.instance.AddScore(pointsOnDestroy);
		}
	}

	// When hit by a bullet, get damaged and destroy the bullet.
	private void OnCollisionEnter2D(Collision2D pColl) {
		if (pColl.gameObject.tag.Equals("PlayerBullet") && !destroyed) {
			pColl.gameObject.SetActive(false);
			Damage();
		}
	}
}
