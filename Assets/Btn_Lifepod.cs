using UnityEngine;
using System.Collections;

public class Btn_Lifepod : MonoBehaviour {

	GameObject lifePod;
	TweenTransform toTransform;
	GameObject case1;
	GameObject case2;
	GameObject case3;
	GameObject case4;
	GameObject case5;
	GameObject case6;
	
	int buttonFlag;
	// Use this for initialization
	void Start () {
		lifePod = GameObject.Find ("lifepod");
		buttonFlag = 0;
		toTransform = GetComponent<TweenTransform> ();
		case1 = GameObject.Find ("Case1");
		case2 = GameObject.Find ("Case2");
		case3 = GameObject.Find ("Case3");
		case4 = GameObject.Find ("Case4");
		case5 = GameObject.Find ("Case5");
		case6 = GameObject.Find ("Case6");
		hideCase ();
	}
	
	private Transform selectTo() {
		toTransform.from = lifePod.transform;
		switch (buttonFlag) {
		case 0:
			return GameObject.Find("Pod0").transform;
			break;
		case 1:
			return GameObject.Find("Pod1").transform;
			break;
		case 2:
			return GameObject.Find("Pod2").transform;
			break;
		case 3:
			return GameObject.Find("Pod3").transform;
			break;
		case 4:
			return GameObject.Find("Pod4").transform;
			break;
		case 5:
			return GameObject.Find("Pod5").transform;
			break;
		case 6:
			return GameObject.Find("Pod6").transform;
			break;
		default:
			return GameObject.Find("Pod0").transform;
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

	public void hideCase() {
		case1.SetActive (false);
		case2.SetActive (false);
		case3.SetActive (false);
		case4.SetActive (false);
		case5.SetActive (false);
		case6.SetActive (false);
	}

	public void Play (bool forward)
	{
		hideCase ();
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

		hideCase ();

		switch (buttonFlag) {
		case 1:
			case1.SetActive(true);
			break;
		case 2:
			case2.SetActive(true);
			break;
		case 3:
			case3.SetActive(true);
			break;
		case 4:
			case4.SetActive(true);
			break;
		case 5:
			case5.SetActive(true);
			break;
		case 6:
			case6.SetActive(true);
			break;
		}
	}
}
