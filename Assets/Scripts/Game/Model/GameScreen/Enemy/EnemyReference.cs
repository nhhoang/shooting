using UnityEngine;
using System.Collections.Generic;

public class EnemyReference : MonoBehaviour {
	public enum Type {
		A = 1,
		B,
		C,
		D
	}
	
	public int level;
	public Type type;
	public int point;
	public float minSpeed;
	public float maxSpeed;
	public float minSize;
	public float maxSize;
	public int numBreakBullet;
	public float ratioAppear;
	
	private static List<EnemyReference> listEnemies = new List<EnemyReference>();
	
	public EnemyReference(int type, int level, int point, float minSpeed, float maxSpeed, float minSize, float maxSize, int numBreakBullet) {
		this.level = level;
		this.type = (Type)type;
		this.point = point;
		this.minSpeed = minSpeed;
		this.maxSpeed = maxSpeed;
		this.minSize = minSize;
		this.maxSize = maxSize;
		this.numBreakBullet = numBreakBullet;
	}
	
	public static void Init() {
  	listEnemies.Clear();
  	int numRows = 0;	
		string[,] output = CSVReader.GetDataReference("Enemy", out numRows);
  	for (int i = 0; i < numRows; i++) {
  		listEnemies.Add(new EnemyReference(int.Parse(output[i, 0]),	
                                         int.Parse(output[i, 1]),
                                         int.Parse(output[i, 2]),
                                         float.Parse(output[i, 3]),
                                         float.Parse(output[i, 4]),
                                         float.Parse(output[i, 5]),
                                         float.Parse(output[i, 6]),
                                         int.Parse(output[i, 7])));
  	}
  }
  
  public static void SetRatioAppear(int level, int type, float ratioAppear) {
  	int count = listEnemies.Count;
  	for (int i = 0; i < count; i++) {
  		if ((int)listEnemies[i].type == type && listEnemies[i].level == level) {
  			listEnemies[i].ratioAppear = ratioAppear;
  			
  			return;
  		}
  	}
  }
	
	public static EnemyReference Get(Type type) {
  	int count = listEnemies.Count;
  	for (int i = 0; i < count; i++) {
  		if (listEnemies[i].type == type) {
  			return listEnemies[i];
  		}
  	}
  	
  	Debug.Log("listEnemies Get Error " + type);
  	return null;
  }
}
