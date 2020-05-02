using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// PUBLIC
	public Vector2 dir;
	public float speed;
	public float lifeTime;

	// PRIVATE

	// COMPONENT
	private Transform tm;

	private void Awake() {
		tm = GetComponent<Transform>();
	}

	private void OnEnable() {
		Invoke("Destroy", lifeTime);
	}

	private void Update() {
		tm.Translate(dir * speed * Time.deltaTime);
	}

	private void Destroy() {
		this.gameObject.SetActive(false);
	}

	private void OnDisable() {
		CancelInvoke();
	}
}
