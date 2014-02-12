using UnityEngine;
using System.Collections;

public class EnemyVisibleCheck : Visiblecheck {
	public override void Init(Enemy enemy) {
		thisEnemy = enemy;
		destroyed = false;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.layer == 10) {
			return;
		}
		Vector3 bulletDirection = transform.position - collision.contacts[0].point;
		Collider c = thisEnemy.CheckForReward();
		LevelManager.Instance.AddPoint(thisEnemy.Reference.point);
		Destroy(bulletDirection);
		foreach (ContactPoint contact in collision.contacts) {
			contact.otherCollider.transform.parent.GetComponent<Bullet>().Explode(transform.position, bulletDirection, c);
		}
	}
	
	public override void Destroy(Vector3 direction) {
		if (!destroyed) {
			destroyed = true;
			thisEnemy.Destroy(direction);
		}
	}
}
