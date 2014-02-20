using UnityEngine;
using System.Collections;

public class FallingObjectVisibleCheck : Visiblecheck {
	public override void Init(FallingObject fallingObject) {
		thisFallingObject = fallingObject;
		destroyed = false;
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.layer == 10) {
			return;
		}

		Destroy(true);
	}
	
	public override void Destroy(bool isKilled = true) {
		if (!destroyed) {
			destroyed = true;
			thisFallingObject.Disapear(isKilled);
		}
	}
}
