using UnityEngine;
using System.Collections;

public class Open : MonoBehaviour {

	Animator animator;
	bool doorOpen;
	// Use this for initialization
	void Start () {
		doorOpen = false;
		animator = GetComponent<Animator> ();
	}

	public void onClick() {
		if (doorOpen) {
			animator.SetTrigger ("Close");
			doorOpen = false;
		} else {
			animator.SetTrigger ("Open");
			doorOpen = true;
		}
	}
}
