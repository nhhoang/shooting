using UnityEngine;
using System.Collections;

public class ItemVisibleCheck : Visiblecheck {
	public override void Init(Item item) {
		thisItem = item;
		destroyed = false;
	}

	void OnCollisionEnter(Collision collision) {
		// Hit reward
		thisItem.GetReward();
		Destroy(Vector3.zero);
//		if (collision.gameObject.layer == 10) {
//			return;
//		}
//		Vector3 bulletDirection = transform.position - collision.contacts[0].point;
//		thisEnemy.CheckForReward();
//		Destroy(bulletDirection);
//		foreach (ContactPoint contact in collision.contacts) {
//			contact.otherCollider.transform.parent.GetComponent<Bullet>().Destroy(transform.position, bulletDirection);
//		}
	}
	
	public override void Destroy(Vector3 direction) {
		if (!destroyed) {
			destroyed = true;
			thisItem.Destroy();
		}
	}
}
