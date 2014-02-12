using UnityEngine;
using System.Collections;

public class Visiblecheck : MonoBehaviour {
	protected Enemy thisEnemy;
	protected Bullet thisBullet;
	protected Item thisItem;
	protected bool destroyed = true;
	
	public virtual void Init(Enemy enemy) {}
	
	public virtual void Init(Bullet bullet) {}
	
	public virtual void Init(Item item) {}
	
	public virtual void Destroy(Vector3 direction) {	}

	public virtual void OnBecameInvisible() {
		if (Application.isPlaying && !destroyed) {
			Destroy(Vector3.zero);
		}
	}
	
	void OnApplicationQuit() {
		destroyed = true;
	}
}
