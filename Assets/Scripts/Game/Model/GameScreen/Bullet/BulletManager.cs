using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletInventory {
	public Bullet.Type type;
	public int total;
	
	public BulletInventory(Bullet.Type type, int total) {
		this.type = type;
		this.total = total;
	}
}

public class BulletManager : MonoBehaviour {
	public static BulletManager Instance;
	
	private BulletInventory tmpBulletInventory;
	private Bullet tmpBullet;
	private Transform trans;
	private Transform obj;
	private GameObject go;
	private Controller controller;
	
	private Bullet.Type currentType;
	private List<BulletInventory> listBullet = new List<BulletInventory>();
	
	private float reloadTime = 1.5f;
	private float totalElapsedSeconds = 0.0f;
	
	void Awake() {
		trans = transform;
		currentType = Bullet.Type.NORMAL;
		Physics.gravity = new Vector3(0, -9.0f, 0);
	}
	
	public void Init(Controller controller) {
		Instance = this;
		this.controller = controller;
		Add(Bullet.Type.NORMAL, 100);
		SetCurrentBulletType(Bullet.Type.NORMAL);
	}
	
	public void SetCurrentBulletType(Bullet.Type type, int numBullet = 100) {
		currentType = type;
		switch (currentType) {
			case Bullet.Type.NORMAL:
				reloadTime = 0.5f;
			break;
		}
		
		GameScreenUI.Instance.AddBullet(type, numBullet);
	}
	
	void Update() {
		totalElapsedSeconds += Time.deltaTime;
	}
	
	private bool CanFire() {
		return totalElapsedSeconds > reloadTime;
	}
	
	public void Fire(Vector3 direction) {
		if (!IsBulletInStock(currentType) || !CanFire()) {
			return;
		}
		
		switch (currentType) {
			case Bullet.Type.NORMAL:
				go = new GameObject("NormalBulletContainer");
				obj = MyPoolManager.Spawn("NormalBullet");
				tmpBullet = go.AddComponent<NormalBullet>();
					
			break;
		}
		
		tmpBullet.Init(currentType, obj, this);	
		obj.gameObject.layer = 9;
		Remove(currentType, 1);
		Utils.SetParent(go.transform, obj);
		Utils.SetParent(controller.gunTrans, go.transform);
		tmpBullet.Fire(direction);
		GameScreenUI.Instance.ReloadBullet();
		
		totalElapsedSeconds = 0.0f;
	}
	
	// Fire bullet when hit to enemy, will not reduce bullet quatity
	public void Fire(Vector3 pos, Vector3 direction, Bullet.Type type, Collider collider) {
		go = new GameObject("NormalBulletContainer");
		obj = MyPoolManager.Spawn("NormalBullet");
		if (collider != null) {
			Physics.IgnoreCollision(obj.collider, collider);
		}
		tmpBullet = go.AddComponent<NormalBullet>();
		tmpBullet.Init(Bullet.Type.NORMAL, obj, this, false);				
		
		obj.gameObject.layer = 11;
		Utils.SetParent(go.transform, obj);
		go.transform.position = pos;
		tmpBullet.Fire(direction, Random.Range(0.2f, 0.4f));
	}
	
	public void Explode(Vector3 pos, Vector3 direction, Bullet.Type type, Collider collider) {
		int numBulletCanInvoke = 1;
		switch (type) {
			case Bullet.Type.NORMAL:
				numBulletCanInvoke = 3;
			break;
		}

		Vector3 nextDirection = Vector3.zero;
		for (int i = 0; i < numBulletCanInvoke; i++) {
			nextDirection = Utils.GetRandomCirclePointXY(pos, 1.0f, 0, 360) - pos;
			Fire(pos, nextDirection, type, collider);
		}
	}
	
	public bool IsBulletInStock(Bullet.Type type) {
		int count = listBullet.Count;
		
		for (int i = 0; i < count; i++) {
			tmpBulletInventory = listBullet[i];
			if (tmpBulletInventory.type == type && tmpBulletInventory.total > 0) {
				return true;
			}
		}
		
		return false;
	}
	
	// Add bullet to eventory, numBullet is the number bullet player can shot
	public void Add(Bullet.Type type, int numBullet = 1) {
		int count = listBullet.Count;
		for (int i = 0; i < count; i++) {
			tmpBulletInventory = listBullet[i];
			if (tmpBulletInventory.type == type) {
				tmpBulletInventory.total += numBullet;
				
				return;
			}
		}
		
		listBullet.Add(new BulletInventory(type, numBullet));
	}
	
	public void Remove(Bullet.Type type, int numBullet = 1) {
		int count = listBullet.Count;
		for (int i = 0; i < count; i++) {
			tmpBulletInventory = listBullet[i];
			if (tmpBulletInventory.type == type) {
				tmpBulletInventory.total -= numBullet;
			}
		}
	}
}
