/** Jonathan So, jonso.gamedev@gmail.com
 * A pooled feedback effect object, used in conjunction with FXManager.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour {

	public string animName;
	private Animator anim;

	private void Awake() {
		anim = GetComponent<Animator>();
	}

	private void OnEnable() {
		anim.Play(animName);
		Invoke("DestroySelf", anim.GetCurrentAnimatorStateInfo(0).length);
	}

	private void DestroySelf() {
		this.gameObject.SetActive(false);
	}
}
