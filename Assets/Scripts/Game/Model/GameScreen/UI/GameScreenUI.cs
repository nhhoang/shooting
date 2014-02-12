using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameScreenUI : MonoBehaviour {
	public static GameScreenUI Instance;

	public UIEventTrigger btnPause;
	public UILabel lblScore;
	public GameObject bottomLeft;
	public BulletUI currentBullet;
	public GameObject prefabBulletUI;
	
	private List<BulletUI> listBullets;
	
	public void Init() {
		Instance = this;
		listBullets = new List<BulletUI>();
		AddBullet(Bullet.Type.NORMAL, 100);
	}
	
	public void SetScore(string value) {
		lblScore.text = value;
	}
	
	public void ReloadBullet() {
		currentBullet.Reload();
	}
	
	public void AddBullet(Bullet.Type type, int quantity) {
		// Check if this type already exist on list
		int count = listBullets.Count;
		for (int i = 0; i < count; i++) {
			if (listBullets[i].bulletType == type) {
				// rePosition it
				currentBullet = listBullets[i];
				currentBullet.Init(type, quantity);
				
				return;
			}
		}
	
		currentBullet = NGUITools.AddChild(bottomLeft, prefabBulletUI).GetComponent<BulletUI>();
		currentBullet.transform.localPosition = new Vector3(30.0f + (count * (60.0f + 15.0f)), 30.0f, 0.0f);
		currentBullet.Init(type, quantity);
		listBullets.Add(currentBullet);
	}
	
	public void SetCurrentBullet(BulletUI bulletUI) {
		currentBullet = bulletUI;
	}
	
	public void EventPauseResume() {
		
	}
}
