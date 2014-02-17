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
	private Transform objectContainer;
	private Transform obj;
	private GameObject go;
	private float xPosRange;
	private float yPosRange;
	private float randomBigSize;
	
	// Falling object properties
	private int objType = 0;
	private float fallSpeed = 10f;
	private int fallOrbitType = 0;   // 0 - straight down, 1 - ziczac
	private float fallRadius = 1;
	private int gainedPoints = 2;
	private int ziczacChance = 45;
	private int ziczacRandom = 0;
	private Renderer render;
	public static Color[] colors = new Color[9];
	private int randomBonusObject = 0;
	
	public void Init(FallingController manager) {
		myController = manager;
		objectContainer = new GameObject("ObjectContainer").transform;
		objectContainer.parent = transform;
		xPosRange = (float)Screen.width / 4 + 12f;
		yPosRange = (float)Screen.height / 4 + 20f;
		colors[0] = Color.black;
		colors[1] = Color.blue;
		colors[2] = Color.cyan;
		colors[3] = Color.gray;
		colors[4] = Color.green;
		colors[5] = Color.magenta;
		colors[6] = Color.red;
		colors[7] = Color.yellow;
		colors[8] = Color.white;
		Invoke("SetBonusObject", Random.Range(5, 10));
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
	
	// For interval 5 - 10s, make an object as bonus that can gain 10 times of normal points
	void SetBonusObject() {
		randomBonusObject = Random.Range(0, numObjects);
		if (numObjects > 0 && fallingObjects[randomBonusObject].CanBeBonusObject()) {
			Invoke("SetBonusObject", Random.Range(5, 10));
		} else {
			Invoke("SetBonusObject", 0.2f);
		}
	}
	
	// Add objects to list
	public void AddObject(GroupType groupType) {
		// Instatiate falling object		
		objType = Random.Range(1, 9);  // We will use 8 correspond colors for random object type
		Debug.Log(Time.time + " adding object------ " + groupType + " object type " + objType + " color: " + colors[objType - 1]);		
		go = new GameObject("FalliingObject");
		obj = MyPoolManager.Spawn("NormalBullet");
		fallingObject = go.AddComponent<FallingObject>();
		obj.gameObject.layer = 9;
		Utils.SetParent(go.transform, obj);
		Utils.SetParent(objectContainer, go.transform);
		go.transform.position = new Vector3(Random.Range(-xPosRange, xPosRange), yPosRange, 0);
		render = obj.GetComponent<Renderer>();
		
			
		switch(groupType) {
			case GroupType.NORMAL:
				ziczacRandom = Random.Range(0, 100);
				fallSpeed = Random.Range(1f, 2.5f) * 10;
				if (ziczacChance >= ziczacChance) {
					fallOrbitType = 1;
					fallRadius = Random.Range(1f, 3f) * 10;
				} else {
					fallOrbitType = 0;
				}
				gainedPoints = 2;
				render.material.SetColor("_TintColor", colors[objType - 1]);
			break;
			
			case GroupType.BIG:
				ziczacRandom = Random.Range(0, 100);
				fallSpeed = Random.Range(2f, 4f) * 10;
				if (ziczacChance >= ziczacChance) {
					fallOrbitType = 1;
					fallRadius = Random.Range(1f, 2f) * 10;
				} else {
					fallOrbitType = 0;
				}
				gainedPoints = Random.Range(3, 6);
				render.material.SetColor("_TintColor", colors[objType - 1]);
				
				// Random object size for big falling object
				randomBigSize = Random.Range(1.2f, 1.8f);
				obj.transform.localScale = new Vector3(obj.transform.localScale.x * randomBigSize, obj.transform.localScale.y * randomBigSize, obj.transform.localScale.z);
			break;
			
			case GroupType.FREEZE:
				fallOrbitType = 0;
				fallSpeed = Random.Range(1.5f, 4.5f) * 10;
				gainedPoints = 2;
				render.material.SetColor("_TintColor", colors[8]);
				randomBigSize = Random.Range(1.5f, 1.8f);
				obj.transform.localScale = new Vector3(obj.transform.localScale.x * randomBigSize, obj.transform.localScale.y * randomBigSize, obj.transform.localScale.z);
			break;
			
			case GroupType.EXPLODE:
				fallOrbitType = 0;
				fallSpeed = Random.Range(2.5f, 5f) * 10;
				gainedPoints = 5;
				render.material.SetColor("_TintColor", colors[objType - 1]);
				randomBigSize = Random.Range(1.5f, 1.8f);
				obj.transform.localScale = new Vector3(obj.transform.localScale.x * randomBigSize, obj.transform.localScale.y * randomBigSize, obj.transform.localScale.z);
			break;
		}
		
		fallingObject.Init(this, obj, render, objType, (int)groupType, fallSpeed, fallOrbitType, fallRadius, gainedPoints);
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
