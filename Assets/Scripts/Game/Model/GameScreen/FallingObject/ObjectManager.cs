using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour {
	public enum Type {
		NORMAL,
		BIG,
		SPECIAL
	}
	
	public enum GroupType {
		NORMAL = 0,
		BIG,
		FREEZE,
		EXPLODE
	}
	
	private List<FallingObject> fallingObjects = new List<FallingObject>();
	private FallingObject fallingObject;
	private int numObjects = 0;
	private int iVar;
	private int tmpVar;
	private FallingController myController;
	private float startFreezeTime;
	private int freezeDuration = 5;
	private bool isFreezing = false;
	
	public void Init(FallingController manager) {
		myController = manager;
	}
	
	public void DoUpdate() {
		for (iVar = 0; iVar < numObjects; iVar++) {
			if (!fallingObjects[iVar].DoUpdate()) {
				fallingObjects.RemoveAt(iVar);
				iVar--;
				numObjects--;
			}
		} 
	}
	
	// Add objects to list
	public void AddObject(ObjectManager.GroupType objectType) {
		Debug.Log(Time.time + " adding object------ " + objectType);
		return;
		// @TODO: instatiate game object
		fallingObjects.Add(fallingObject);
		numObjects++;
	}
	
	// Event: Destroy all objects that have same type
	public void ExplodeObjectSameType(int explodeType, bool isShoot) {
		myController.ExplodeObjectDestroy();
		if (isShoot) {
			for (tmpVar = 0; tmpVar < numObjects; tmpVar++) {
				if (fallingObjects[tmpVar].IsSameType(explodeType)) {
					fallingObjects[tmpVar].Disapear(true);
				}
			}
		}
	}
	
	// Event: Freeze object get shoot
	public void EventFreezeObject(bool isShoot) {
		myController.FreezeObjectDestroy(isShoot);
		
		if (isShoot) {
			startFreezeTime = Time.time + freezeDuration;
			if (!isFreezing) {
				InvokeRepeating("CheckFreezing", 0, 0.1f);
				isFreezing = true;
			}
		
		
			for (tmpVar = 0; tmpVar < numObjects; tmpVar++) {
				fallingObjects[tmpVar].ChangeSpeed(true);
			}
		}
	}
	
	void CheckFreezing() {
		if (Time.time >= startFreezeTime) {
			CancelInvoke("CheckFreezing");
			isFreezing = false;
			
			for (tmpVar = 0; tmpVar < numObjects; tmpVar++) {
				fallingObjects[tmpVar].ChangeSpeed(false);
			}
		}
	}
}
