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
	private Color nmeColor; // The color corresponding to us as an enemy; lerped from yellow (super easy) to green (easy) to cyan (medium) to magenta (hard).

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
	}

	// Set up the color of the enemy based on the threat level.
	private void OnEnable() {
		float threat = ThreatLevel.instance.GetThreatPercent();
		if (threat < 1/3f) {
			nmeColor = Color.Lerp(Color.yellow, Color.green, (threat / (1/3f)));
		} else if (threat < 2/3f) {
			nmeColor = Color.Lerp(Color.green, Color.cyan, ((threat - (1/3f)) / (1/3f)));
		} else {
			nmeColor = Color.Lerp(Color.cyan, Color.magenta, ((threat - (2/3f)) / (1/3f)));
		}
		sr.color = nmeColor;
		SetupFlashPalette();
	}

	// Sets up the colors to go through when hit.
	private void SetupFlashPalette() {
		flashColors = new List<Color>();
		for (int i = 0; i < FLASH_FRAMES; i++) {
			flashColors.Add(Color.Lerp(Color.red, nmeColor, (i / (float) FLASH_FRAMES)));
		}
	}

	// When hit, decrease health and add points, then check for destruction.
	private void Damage() {
		health--;
		ScoreManager.instance.AddScore(pointsOnHit);
		Vector2 randPos = new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f));
		FXManager.instance.ShowFX("hitspark", randPos, transform.localScale);
		if (!hidden) { StartCoroutine(ColorFlash()); }
		SFXManager.instance.PlayClip("hit");
		DestroyCheck();
	}

	private IEnumerator ColorFlash() {
		for (int i = 0; i < FLASH_FRAMES; i++) {
			sr.color = flashColors[i];
			yield return new WaitForEndOfFrame();
		}
		sr.color = nmeColor;
	}

	// If destroyed, then stop shooting, stop moving, and set the flags to indicate destruction.
	private void DestroyCheck() {
		if (health <= 0 && !destroyed) {
			destroyed = true;
			coll.enabled = false;
			sr.sprite = destroySprite;
			// Disable shooting.
			Exploder exp = GetComponent<Exploder>();
			EnemyAim nma = GetComponent<EnemyAim>();
			Tank tnk = GetComponent<Tank>();
			Cannon can = GetComponent<Cannon>();

			if (exp) { exp.StopShoot(); }
			if (nma) { nma.StopShoot(); }
			if (tnk) { tnk.StopShoot(); }
			if (can) { can.StopShoot(); }

			// Display the explosion effect. 
			FXManager.instance.ShowFX("explosion", transform.position, transform.localScale);
			// Add to score.
			ScoreManager.instance.AddScore(pointsOnDestroy);

			SFXManager.instance.PlayClip("destroy");

			// Make note of it with the Threat Level.
			ThreatLevel.instance.AddKillLevel(1/2f);
		}
	}

	// When hit by a bullet, get damaged and destroy the bullet.
	private void OnCollisionEnter2D(Collision2D pColl) {
		if (pColl.gameObject.tag.Equals("PlayerBullet") && !destroyed) {
			pColl.gameObject.SetActive(false);
			Damage();
		} else if (pColl.gameObject.tag.Equals("Player") && !destroyed) { // If we touch the player, receive damage.
			Damage();
		}
	}
}
