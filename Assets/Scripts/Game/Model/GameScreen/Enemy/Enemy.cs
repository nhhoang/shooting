using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	protected EnemyReference.Type type;
	protected EnemyReference reference;
	protected Transform trans;
	protected Transform child;
	protected Rigidbody rig;
	protected Vector3 direction = Vector3.zero;
	protected Vector3 tmpVector3;
	private float totalSecondsElapsed = 0.0f;
	private float timeToDestroy = 55.0f;
	private Vector3 speed = new Vector3(0.0f, 20.0f, 0.0f);
	
	public virtual void Init(EnemyReference.Type type, Transform child) {
		this.type = type;
		reference = EnemyReference.Get(type);
		trans = transform;
		this.child = child;
		speed.y *= Random.Range(reference.minSpeed, reference.maxSpeed);
		child.GetComponent<EnemyVisibleCheck>().Init(this);
		rig = child.rigidbody;
		rig.isKinematic = false;
		rig.velocity = Vector3.zero;
	}
	
	void FixedUpdate() {
		DoFixedUpdate();
	}
	
	public virtual void DoFixedUpdate() {
		totalSecondsElapsed += Time.deltaTime;
		rig.MovePosition(rig.position - Time.deltaTime * speed);
	}
	
	public Rect GetRect() {
		Bounds bound = child.renderer.bounds;
		return new Rect(bound.min.x, bound.max.y, bound.size.x, bound.size.y);
	}
	
	public EnemyReference Reference {
		get {return reference;}
	}
	
	public void SetPosition(Vector3 v) {
		trans.position = v;
	}
	
	public Collider CheckForReward() {
		return ItemManager.Instance.CheckForBonus(type, rig.position);
	}
	
	public void Destroy(Vector3 bulletDirection) {
		MyPoolManager.Despawn(child);
		Destroy(gameObject);
	}
}