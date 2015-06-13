/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2014.                                   *
* Leap Motion proprietary. Licensed under Apache 2.0                           *
* Available at http://www.apache.org/licenses/LICENSE-2.0.html                 *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;

// The model for our rigid hand made out of various polyhedra.
public class RigidHand : SkeletalHand {

  public float filtering = 0.5f;

  Controller controller;
  HandList mhands;
  FingerList mfingers;
  int extendedFingers;
  GameObject lifePod;
  float mpitch = 0.0f;
  float myaw = 0.0f;
  GameObject canvasObj = GameObject.Find ("Canvas");

  void Start() {
    palm.GetComponent<Rigidbody>().maxAngularVelocity = Mathf.Infinity;
    Leap.Utils.IgnoreCollisions(gameObject, gameObject);
	controller = new Controller ();
	lifePod = GameObject.Find ("lifepod");
  }

  void Update () {
	Frame frame = controller.Frame ();
	mhands = frame.Hands;
		
	extendedFingers = 0;
	for (int i = 0; i < mhands.Count; i++) {
		Hand hand = mhands[i];
		for (int j = 0; j < hand.Fingers.Count; j++) {
			Finger digit = hand.Fingers[j];
			if(digit.IsExtended) extendedFingers ++;
		}
	}
		
	//print ("Hans "+mhands.Count+"::::"+extendedFingers);


		for (int f = 0; f < fingers.Length; ++f) {
			if (fingers[f] != null)
				fingers[f].UpdateFinger();
		}
		
		if (palm != null) {
			// Set palm velocity.
			//print (":=========================" + extendedFingers);
			Vector3 target_position = GetPalmCenter();
			palm.GetComponent<Rigidbody>().velocity = (target_position - palm.transform.position) *
				(1 - filtering) / Time.deltaTime;
			Vector3 tran_pos = target_position - palm.transform.position;
			print ("mPitch:" + tran_pos.y*500 + "   mYaw:" + tran_pos.x * 500);
			if ((extendedFingers > 5 && extendedFingers <= 10)) {
				tran_pos.z = -0.1f * tran_pos.z;
				tran_pos.x = 0;
				tran_pos.y = 0;
				Camera.main.transform.Translate(tran_pos * Time.deltaTime*100);
			//} else if ((extendedFingers > 5 && extendedFingers <= 10)) {
			//	tran_pos.z = 0;
			//	tran_pos.x = -0.2f * tran_pos.x;
			//	tran_pos.y = -0.2f * tran_pos.y;
			} else if ((extendedFingers > 2 && extendedFingers <= 5)) {
				GameObject canvasObj = GameObject.Find("Canvas");
				if (canvasObj.tag == "ar") {
					Btn_Lifepod lifepod_script = lifePod.GetComponent<Btn_Lifepod> ();
					lifepod_script.onTouch();
					lifepod_script.hideCase();
					Btn_Camera camera_script = Camera.main.GetComponent<Btn_Camera> ();
					camera_script.onTouch();
					return;
				}

				lifePod.transform.RotateAround(Vector3.zero, lifePod.transform.up, Time.deltaTime * 500.0f * tran_pos.x * (-1)/*,  Space.World*/);

				lifePod.transform.RotateAround(Vector3.zero, Vector3.left, Time.deltaTime * 500.0f * tran_pos.y * (-1)/*,  Space.World*/);
				tran_pos.z = -0.1f * tran_pos.z;
				tran_pos.x = 0;
				tran_pos.y = 0;
				Camera.main.transform.Translate(tran_pos * Time.deltaTime*100);
				//}

			}
			

		}
  }

  public override void InitHand() {
    base.InitHand();
  }

  public override void UpdateHand() {
    
  }
}
