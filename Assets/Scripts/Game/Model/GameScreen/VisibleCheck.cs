using UnityEngine;
using System.Collections;

public class Visiblecheck : MonoBehaviour {
	protected Bullet thisBullet;
	protected FallingObject thisFallingObject;
	protected bool destroyed = true;
	
	public virtual void Init(Bullet bullet) {}
	
	public virtual void Init(FallingObject thisFallingObject) {}
	
	public virtual void Destroy(bool isKilled = true) {	}

	public virtual void OnBecameInvisible() {
		if (Application.isPlaying && !destroyed) {
			Destroy(false);
		}
	}
	
	void OnApplicationQuit() {
		destroyed = true;
	}
}
