using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public enum Type {
		NORMAL,
		RAPID,
		BIG_EXPLOSION
	}
	
	public float damage = 1f;
	
	protected Type type;
	protected float speed = 450.0f;
	protected Transform trans;
	protected Transform child;
	protected Rigidbody rig;
	protected BulletManager bulletManager;
	private float timeToDestroy = 5.0f;
	private bool canExplode = true;
	
	public virtual void Init(Type type, Transform child, BulletManager bulletManager, bool canExplode = true) {
		this.type = type;
		trans = transform;
		this.child = child;
		rig = child.rigidbody;
		this.bulletManager = bulletManager;
		this.canExplode = canExplode;
		child.GetComponent<BulletVisibleCheck>().Init(this);
	}
	
	public virtual void Fire(Vector3 direction, float customSpeed = 1.0f) {
		rig.useGravity = true;
		rig.isKinematic = false;
		rig.velocity = direction.normalized * speed * customSpeed;
	}
	
	public void Explode(Vector3 pos, Vector3 direction, Collider collider) {
		bulletManager.Explode(pos, direction, type, collider);
	}
	
	public void Destroy() {
		MyPoolManager.Despawn(child);
		Destroy(gameObject);
	}
}
