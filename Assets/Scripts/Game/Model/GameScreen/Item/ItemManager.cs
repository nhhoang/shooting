using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {
	public static ItemManager Instance;
	
	private Item.Type[] percentArray = new Item.Type[100];
	private int currentPercentArrayIndex = 0;
	private Transform trans;
	private Item tmpItem;
	private GameObject go;
	private Transform obj;
	
	public void Init() {
		Instance = this;
		trans = transform;
		AddToPercentArray(Item.Type.FAST_RECOVERY, 40);
		AddToPercentArray(Item.Type.SCORE, 20);
		AddToPercentArray(Item.Type.BIG_EXPLOSION, 40);
	}
	
	public void AddToPercentArray(Item.Type type, int percent) {
		if (currentPercentArrayIndex >= 100) {
			return;
		}
		
		for (int i = 0; i < percent; i++) {
			percentArray[currentPercentArrayIndex] = type;
			currentPercentArrayIndex++;
			if (currentPercentArrayIndex >= 100) {
				return;
			}
		}
	}
	
	public Item.Type GetRewardType() {
		int randomValue = Random.Range(0, 100);
		return percentArray[randomValue];
	}

	public Collider CheckForBonus(EnemyReference.Type type, Vector3 pos) {
		float percentCanGetReward = 0.0f;
		switch (type) {
			case EnemyReference.Type.A:
				percentCanGetReward = 25.0f;
			break;
			
			case EnemyReference.Type.B:
				percentCanGetReward = 27.0f;
			break;
			
			case EnemyReference.Type.C:
				percentCanGetReward = 29.0f;
			break;
			
			case EnemyReference.Type.D:
				percentCanGetReward = 11.0f;
			break;
		}

		if (Utils.CanGetReward(percentCanGetReward)) {
			return Add(GetRewardType(), pos);
		}
		
		return null;
	}
	
	public Collider Add(Item.Type type, Vector3 pos) {
		go = new GameObject("Item");
		obj = MyPoolManager.Spawn("Item");
		
		Utils.SetParent(go.transform, obj);
		tmpItem = go.AddComponent<Item>();
		Utils.SetParent(trans, go.transform);
		go.transform.position = pos;
		Vector3 nextDirection;
		if (pos.x < 0.0f) {
			nextDirection = Utils.GetRandomCirclePointXY(pos, 1.0f, 45, 90) - pos;
		} else {
			nextDirection = Utils.GetRandomCirclePointXY(pos, 1.0f, 90, 120) - pos;
		}
		tmpItem.Init(type, obj);
		tmpItem.Fire(nextDirection);
		
		return obj.collider;
	}
}
