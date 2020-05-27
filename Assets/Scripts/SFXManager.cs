using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

	// SINGLETON
	public static SFXManager instance;

	// PUBLIC
	public AudioClip shot, hit, destroy;

	// PRIVATE

	// COMPONENT
	private AudioSource audi;

	private void Awake() {
		if (instance == null) { instance = this; }
		audi = GetComponent<AudioSource>();
	}

	public void PlayClip(string type) {
		if (type == "shot") {
			audi.PlayOneShot(shot, 0.25f);
		} else if (type == "hit") {
			audi.PlayOneShot(hit, 0.5f);
		} else {
			audi.PlayOneShot(destroy, 1f);
		}
	}

}
