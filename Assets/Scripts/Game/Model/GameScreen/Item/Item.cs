using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public enum Type {
		FAST_RECOVERY,
		SCORE,
		BIG_EXPLOSION
	}
	
	protected Transform trans;
	protected Transform child;
	protected Rigidbody rig;
	protected Item.Type type;
	private float gravity = 1.0f;
	private Vector3 moveDirection;
	private Vector3 speed = new Vector3(0.0f, 10.0f, 0.0f);
	private float initSpeed = 1.0f;
	private float totalSecondsElapsed = 0.0f;
	
	public virtual void Init(Item.Type type, Transform child) {
		this.type = type;
		trans = transform;
		this.child = child;
		child.GetComponent<ItemVisibleCheck>().Init(this);
		rig = child.rigidbody;
		rig.isKinematic = false;
		rig.velocity = Vector3.zero;
	}
	
	public virtual void Fire(Vector3 direction, float customSpeed = 1.0f) {
//		rig.useGravity = true;
//		rig.isKinematic = false;
//		rig.velocity = direction.normalized * initSpeed * customSpeed;
		moveDirection = direction.normalized * initSpeed * customSpeed;
	}
	
	public void GetReward() {
		switch (type) {
			case Type.FAST_RECOVERY:
				BulletManager.Instance.SetCurrentBulletType(Bullet.Type.RAPID, 20);
			break;
			
			case Type.SCORE:
				LevelManager.Instance.AddPoint(1000);
			break;
			
			case Type.BIG_EXPLOSION:
				BulletManager.Instance.SetCurrentBulletType(Bullet.Type.BIG_EXPLOSION, 20);
			break;
		}
	}
	
	void Update() {
		DoUpdate();
	}
	
	public virtual void DoUpdate() {
		moveDirection.y -= gravity * Time.deltaTime;
		rig.position += moveDirection;
//		totalSecondsElapsed += Time.deltaTime;
//		rig.MovePosition(rig.position - moveDirection);
	}
	
	public void Destroy() {
		MyPoolManager.Despawn(child);
		Destroy(gameObject);
	}
}
