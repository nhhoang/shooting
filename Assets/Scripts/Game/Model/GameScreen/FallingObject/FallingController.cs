using UnityEngine;
using System.Collections;

public class FallingController : MonoBehaviour {
	private int totalTime = 90; // second
	private float passTime = 0;
	private float markTime;
	private float nextSpawnTime;
	private ObjectManager objectManager;
	private int spawnChance;
	private int freezeObjectChance = 80;
	private int explodeObjectChance = 90;
	private int bigObjectChance = 65;
	private int numFreezeObjects = 0;
	private int numExplodeObjects = 0;
	private bool isObjectSpawn = false;
	private bool isGameObject = false;
	
	public void Init() {
		passTime = 0;
		isGameObject = false;
		markTime = Time.time;
		nextSpawnTime = Time.time + 1f;
		objectManager = gameObject.AddComponent<ObjectManager>() as ObjectManager;
		objectManager.Init(this);
	}
	
	public void DoUpdate() {
		if (isGameObject) {
			return;
		}

		objectManager.DoUpdate();
		
		// Checking game time
		passTime = Time.time - markTime;
		if (GameScreen.isTimePlayMode && passTime >= totalTime) {
			EventGameOver();		
		}
		
		if (Time.time >= nextSpawnTime) {
			nextSpawnTime = Time.time + Random.Range(1.8f, 2.5f);
			isObjectSpawn = false;
			
			// At a moment, there is max 1 freeze object, 2 explode objects
			spawnChance = Random.Range(0, 100);
			if (passTime > 8) {
				if (numExplodeObjects < 2) {
				 	if (spawnChance >= explodeObjectChance) {
						// Spawn explode object
						isObjectSpawn = true;
						numExplodeObjects++;
						explodeObjectChance = 90;
						SpawnObject(ObjectManager.GroupType.EXPLODE);
				 	} else {
				 		explodeObjectChance -= 1;
				 	}
				}
			 	
			 	if (numFreezeObjects < 1) {
			 		if (!isObjectSpawn && spawnChance >= freezeObjectChance) {
						// Spawn freeze object
						isObjectSpawn = true;
						numFreezeObjects = 1;
						freezeObjectChance = 80;
						SpawnObject(ObjectManager.GroupType.FREEZE);
			 		} else {
			 			freezeObjectChance -= 1;
			 		}
			 	}
			}
			
			if (!isObjectSpawn) {
				if (passTime > 5 && spawnChance >= bigObjectChance) {
					// Spawn big object
					isObjectSpawn = true;
					SpawnObject(ObjectManager.GroupType.BIG);
				}
				
				if (!isObjectSpawn) {
					// Spawn normal object
					SpawnObject(ObjectManager.GroupType.NORMAL);
				}
			}
		}
	}
	
	public int GetRemainTime() {
		return (int)(totalTime - passTime);
	}
	
	public void SpawnObject(ObjectManager.GroupType objectType) {
		objectManager.AddObject(objectType);
	}
	
	// Freeze Object destroy
	public void FreezeObjectDestroy(bool isShoot) {
		if (isShoot && GameScreen.isTimePlayMode) {
			passTime = Mathf.Min(passTime - 3, 0);
		}
		numFreezeObjects = 0;
	}
	
	// Explode Object destroy
	public void ExplodeObjectDestroy() {
		numExplodeObjects = Mathf.Min(numExplodeObjects - 1, 0);
	}
	
	// Game runs out of time
	public void EventGameOver() {
		//@TODO: show game result screen
		isGameObject = true;
		Debug.Log ("::::::Game is over, should show result screen, currentScore " + GameData.GetCurrentScore() + " bestScore " + GameData.GetBestScore());
	}
}
