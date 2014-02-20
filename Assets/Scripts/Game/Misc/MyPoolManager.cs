using UnityEngine;
using System.Collections;

public class MyPoolManager : MonoBehaviour {
	private static SpawnPool spawnPool = null;

	public static bool alreadyInit = false;
	private static string poolName = "ObjectsGame";
	private static Transform trans;
	
	public static void Init() {
		if (!alreadyInit) {
  		if (!PoolManager.Pools.TryGetValue(poolName, out spawnPool)) PoolManager.Pools.Create(poolName);
  		CommonPrefab();
  		alreadyInit = true;
		}
	}
	
	public static void CommonPrefab() {
		CreatePrefab(poolName, "Prefabs/Screens/GameScreen/Bullets/NormalBullet", 10, 10);
		CreatePrefab(poolName, "Prefabs/Screens/GameScreen/Enemies/NormalEnemy", 10, 10);
		CreatePrefab(poolName, "Prefabs/Screens/GameScreen/UI/GameScreenUI", 1, 1);
		CreatePrefab(poolName, "Prefabs/Screens/GameScreen/UI/BulletUI", 5, 5);
	}
	
	public static void CreatePrefab(string poolName, string prefabPath, int preload, int cullAbove) {
		PrefabPool prefabPool = new PrefabPool((Resources.Load(prefabPath) as GameObject).transform);
		prefabPool.preloadAmount = preload;			// This is the default so may be omitted
//		prefabPool.cullDespawned = false;
		prefabPool.cullAbove = cullAbove;
		prefabPool.cullDelay = 1;
//		prefabPool.limitInstances = false; // This is the default so may be omitted
//		prefabPool.limitAmount = 20;			 // This is the default so may be omitted
		PoolManager.Pools[poolName].CreatePrefabPool(prefabPool);
	}
	
	public static void CreatePrefab(string poolName, Transform obj, int preload, int cullAbove) {
		PrefabPool prefabPool = new PrefabPool(obj);
		prefabPool.preloadAmount = preload;			// This is the default so may be omitted
//		prefabPool.cullDespawned = false;
		prefabPool.cullAbove = cullAbove;
		prefabPool.cullDelay = 1;
//		prefabPool.limitInstances = false; // This is the default so may be omitted
//		prefabPool.limitAmount = 20;			 // This is the default so may be omitted
		PoolManager.Pools[poolName].CreatePrefabPool(prefabPool);
	}
	
	public static Transform Spawn(string prefabName, bool keepPrefabPosition = false, bool setActive = true) {		
		trans = null;
		if (!keepPrefabPosition) {
			trans = PoolManager.Pools[poolName].Spawn(PoolManager.Pools[poolName].prefabs[prefabName], Vector3.zero, PoolManager.Pools[poolName].prefabs[prefabName].rotation);
		} else {
			trans = PoolManager.Pools[poolName].Spawn(PoolManager.Pools[poolName].prefabs[prefabName], PoolManager.Pools[poolName].prefabs[prefabName].position, PoolManager.Pools[poolName].prefabs[prefabName].rotation);
		}
		if (setActive) {
			NGUITools.SetActive(trans.gameObject, true);
		}
		
		trans.name = prefabName;
		
		return trans;
	}
	
	public static void Despawn(Transform objTransform, float delayTime = 0f) {
		objTransform.parent = PoolManager.Pools[poolName].group;
		if (delayTime == 0f) {
			PoolManager.Pools[poolName].Despawn(objTransform);
		} else {
			PoolManager.Pools[poolName].Despawn(objTransform, delayTime);
		}
	}
}
