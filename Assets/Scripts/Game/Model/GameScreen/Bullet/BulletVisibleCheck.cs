using UnityEngine;
using System.Collections;

public class BulletVisibleCheck : Visiblecheck {
	public override void Init(Bullet bullet) {
		thisBullet = bullet;
		destroyed = false;
	}
	
	public override void Destroy(Vector3 direction) {
		if (!destroyed) {
			destroyed = true;
			thisBullet.Destroy();
		}
	}
}
