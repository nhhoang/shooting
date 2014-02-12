using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	private LevelManager levelManager;
	private int currentLevel;
	private Transform trans;
	private EnemyLevelReference enemyLevelRef;
	private int numEnemyPerWave = 3;
	private Camera gameCamera;
	// Variables for generate enemy
	private float secondsToNextWave = 4.0f;
	private float totalElapsedSeconds = 0.0f;
	private float xRange;
	private float yRange = 50.0f;
	private float screenWidth;
	private float leftBound;
	private GameObject go;
	private Transform obj;
	private Enemy tmpEnemy;
	
	public void Init() {
		levelManager = LevelManager.Instance;
		currentLevel = levelManager.currentLevel;
		trans = transform;
		gameCamera = Director.Instance.currentScreen.screenCamera;
		Utils.SetPositionY(trans, 240.0f);
//		Utils.SetPositionY(trans, (float)gameCamera.pixelHeight / 2);
		
		enemyLevelRef = EnemyLevelReference.Get(currentLevel);
		
		screenWidth = 320.0f;//gameCamera.pixelWidth;
		
		xRange = (float)screenWidth / numEnemyPerWave;
		leftBound = -(float)screenWidth / 2;
		
		AddNextWave();
	}
	
	void Update() {
		totalElapsedSeconds += Time.deltaTime;
		if (totalElapsedSeconds > secondsToNextWave) {
			totalElapsedSeconds = 0.0f;
			AddNextWave();
		}
	}
	
	void AddNextWave() {
		Enemy enemy = null;
		Vector3 pos;
		float nextLeftBound = leftBound;// + Random.Range(-xRange, xRange);
		for (int i = 0; i < numEnemyPerWave; i++) {
			enemy = Add(enemyLevelRef.GetRandomEnemy());
			// Generate random position for this enemy
			Rect rect = enemy.GetRect();
			float minX = nextLeftBound + i * xRange + ((float)rect.width / 2);
			float maxX = nextLeftBound + (i + 1) * xRange - ((float)rect.width / 2);
			float minY = rect.center.y - ((float)rect.height / 2);
			float maxY = rect.center.y + ((float)rect.height / 2) + yRange;
			pos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0.0f);
			enemy.SetPosition(pos);
		}
	}
	
	public Enemy Add(EnemyReference.Type type) {
		go = new GameObject("Enemy");
		switch (type) {
			case EnemyReference.Type.A:
				obj = MyPoolManager.Spawn("EnemyA");
			break;
			
			case EnemyReference.Type.B:
				obj = MyPoolManager.Spawn("EnemyB");
			break;
			
			case EnemyReference.Type.C:
				obj = MyPoolManager.Spawn("EnemyC");
			break;
			
			case EnemyReference.Type.D:
				obj = MyPoolManager.Spawn("EnemyD");
			break;
		}
		
		Utils.SetParent(go.transform, obj);
		tmpEnemy = go.AddComponent<Enemy>();
		Utils.SetParent(trans, go.transform);
		tmpEnemy.Init(type, obj);	
		
		return tmpEnemy;
	}
	
	public void Remove(EnemyReference.Type type) {
	}
}
