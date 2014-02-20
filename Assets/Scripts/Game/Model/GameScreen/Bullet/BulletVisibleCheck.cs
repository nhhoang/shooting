using UnityEngine;
using System.Collections;

public class BulletVisibleCheck : Visiblecheck {
	public override void Init(Bullet bullet) {
		thisBullet = bullet;
		destroyed = false;
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.layer == 9) {
			return;
		}

		Destroy(true);
	}
	
	public override void Destroy(bool isKilled = true) {
		if (!destroyed) {
			destroyed = true;
			thisBullet.Destroy();
		}
	}
}
