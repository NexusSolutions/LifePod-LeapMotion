using UnityEngine;
using System.Collections;

public class Btn_Camera : MonoBehaviour {
	
	Camera mainCamera;
	TweenTransform toTransform;

	int buttonFlag;
	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		buttonFlag = 0;
		toTransform = GetComponent<TweenTransform> ();
	}

	private Transform selectTo() {
		toTransform.from = mainCamera.transform;
		switch (buttonFlag) {
		case 0:
			return GameObject.Find("Camera0").transform;
			break;
		case 1:
			return GameObject.Find("Camera1").transform;
			break;
		case 2:
			return GameObject.Find("Camera2").transform;
			break;
		case 3:
			return GameObject.Find("Camera3").transform;
			break;
		case 4:
			return GameObject.Find("Camera4").transform;
			break;
		case 5:
			return GameObject.Find("Camera5").transform;
			break;
		case 6:
			return GameObject.Find("Camera6").transform;
			break;
		default:
			return GameObject.Find("Camera0").transform;
		}
		return null;
	}

	public void onTouch() {
		buttonFlag = 0;
		toTransform.to = selectTo ();
		Play (true);
	}
	
	public void onTouch1() {
		buttonFlag = 1;
		toTransform.to = selectTo ();
		Play (true);
	}
	
	public void onTouch2() {
		buttonFlag = 2;
		toTransform.to = selectTo ();
		Play (true);
	}
	
	public void onTouch3() {
		buttonFlag = 3;
		toTransform.to = selectTo ();
		Play (true);
	}
	
	public void onTouch4() {
		buttonFlag = 4;
		toTransform.to = selectTo ();
		Play (true);
	}
	
	public void onTouch5() {
		buttonFlag = 5;	
		toTransform.to = selectTo ();
		Play (true);
	}
	
	public void onTouch6() {
		buttonFlag = 6;
		toTransform.to = selectTo ();
		Play (true);
	}

	public void Play (bool forward)
	{
		GameObject go = this.gameObject;		
		if (!NGUITools.GetActive(go))
		{
			// Enable the game object before tweening it
			NGUITools.SetActive(go, true);
		}
		
		// Gather the tweening components
		UITweener[] mTweens = go.GetComponentsInChildren<UITweener>();
		
		if (mTweens.Length == 0)
		{
			// No tweeners found -- should we disable the object?
				//NGUITools.SetActive(tweenTarget, false);
		}
		else
		{
			bool activated = false;
			//forward = !forward;
			
			// Run through all located tween components
			for (int i = 0, imax = mTweens.Length; i < imax; ++i)
			{
				UITweener tw = mTweens[i];
				
				// If the tweener's group matches, we can work with it

				// Ensure that the game objects are enabled
				if (!activated && !NGUITools.GetActive(go))
				{
					activated = true;
					NGUITools.SetActive(go, true);
				}

				tw.ResetToBeginning();
				// Listen for tween finished messages
				EventDelegate.Add(tw.onFinished, OnFinished, true);
				tw.Play(forward);

			}
		}
	}

	void OnFinished ()
	{
		GameObject canvasObj = GameObject.Find ("Canvas");
		if (buttonFlag > 0) {
			canvasObj.tag = "ar";
		} else {
			canvasObj.tag = "ra";
		}
	}
}
