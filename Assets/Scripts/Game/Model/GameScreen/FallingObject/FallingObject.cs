using UnityEngine;
using System.Collections;

public class FallingObject : MonoBehaviour {
	private int objectType = 0;
	private int groupType = 0;
	private float fallSpeed = 1f;
	private int fallOrbitType = 0;   // 0 - straight down, 1 - ziczac
	private float fallRadius = 1f;
	private int gainedPoints = 2;
	private int randomType = -1;
	private bool isDestroy = false;
	private Vector3 fallVector = Vector3.down;
	
	private Transform thisTransform;
	private Transform child;
	private ObjectManager manager;
	
	// Init objects properties
	public void Init(ObjectManager mana, Transform childTransform, int objType, int gType, float fSpeed, int fOrbitType, float fRadius, int gPoints) {
		isDestroy = false;
		manager = mana;
		objectType = objType;
		groupType = gType;
		fallSpeed = fSpeed;
		fallOrbitType = fOrbitType;
		fallRadius = fRadius;
		gainedPoints = gPoints;
		if (groupType == (int)ObjectManager.GroupType.EXPLODE) {
			randomType = objectType;
			InvokeRepeating("ChangeRandomType", 0, 0.2f);
		}
		
		if (fallOrbitType == 1) {
			// Change orbit in interval time
			Invoke("ChangeOrbit", 0.2f);
		}
		
		child = childTransform;
		thisTransform = transform;
	}
	
	// Update is called once per frame
	public bool DoUpdate () {
		if (isDestroy) {
			return false;
		}
		
		// Falling to floor
		FallingToFloor();
		
		return true;
	}
	
	void FallingToFloor() {
		if (fallOrbitType == 0) {
			// Fall straight down
			thisTransform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
		} else {
			// Fall in ziczac orbit
			thisTransform.Translate(fallVector * fallSpeed * Time.deltaTime, Space.World);
		}
	}
	
	// Change fall speed
	public void ChangeSpeed(bool isSlowDown) {
		if (isSlowDown) {
			fallSpeed /= 2;
		} else {
			fallSpeed *= 2;
		}
	}
	
	// Change random type for EXPLODE object
	void ChangeRandomType() {
		// @TODO: randomType should reflects in color
		randomType = Random.Range(1, 9);
	}
	
	void ChangeOrbit() {
		Invoke("ChangeOrbit", Random.Range(0.2f, 2.5f));
		fallVector.x = Random.Range(-0.9f, 0.9f);
		fallSpeed += Random.Range(-5f, 5f);
		if (fallSpeed <= 10) {
			fallSpeed = 10f;
		} else if (fallSpeed >= 50) {
			fallSpeed = 50;
		}
	}
	
	// Compare object type
	public bool IsSameType(int targetType) {
		return targetType == objectType;	
	}
	
	// Object get shoot and be destroyed
	public void Disapear(bool isShoot) {
		if (!isDestroy) {
			isDestroy = true;
			CancelInvoke();
			
			if (isShoot) {
				GameData.UpdateScore(gainedPoints);
			}
			
			switch(groupType) {
				case (int)ObjectManager.GroupType.FREEZE:
					manager.EventFreezeObject(isShoot);
				break;
				
				case (int)ObjectManager.GroupType.EXPLODE:
					manager.ExplodeObjectSameType(randomType, isShoot);
				break;
			}
				
			// Despawn game object
			MyPoolManager.Despawn(child);
			Destroy(gameObject); 
		}
	}
}
