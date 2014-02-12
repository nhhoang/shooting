using UnityEngine;
using System.Collections.Generic;

public class EnemyLevelReference : MonoBehaviour {
	private int level;
	private float ratioEnemyA;
	private float ratioEnemyB;
	private float ratioEnemyC;
	private float ratioEnemyD;
	private int[] ratioList = new int[100];
	
	private static List<EnemyLevelReference> listEnemyLevel = new List<EnemyLevelReference>();
	
	public EnemyLevelReference(int level, int type1, float ratio1, int type2, float ratio2, int type3, float ratio3, int type4, float ratio4) {
		ratioList = new int[100];
		this.level = level;
		ratioEnemyA = ratio1;
		EnemyReference.SetRatioAppear(level, type1, ratio1);
		// add to ratiolist
		int index = 0;
		int count = Mathf.FloorToInt(ratio1 * 100.0f);
		for (int i = 0; i < count; i++) {
			ratioList[index] = type1;
			index++;
		}
		ratioEnemyB = ratio2;
		EnemyReference.SetRatioAppear(level, type2, ratio2);
		// add to ratiolist
		count = Mathf.FloorToInt(ratio2 * 100.0f);
		for (int i = 0; i < count; i++) {
			ratioList[index] = type2;
			index++;
		}
		ratioEnemyC = ratio3;
		EnemyReference.SetRatioAppear(level, type3, ratio3);
		// add to ratiolist
		count = Mathf.FloorToInt(ratio3 * 100.0f);
		for (int i = 0; i < count; i++) {
			ratioList[index] = type3;
			index++;
		}
		ratioEnemyD = ratio4;
		EnemyReference.SetRatioAppear(level, type4, ratio4);
		// add to ratiolist
		count = Mathf.FloorToInt(ratio4 * 100.0f);
		for (int i = 0; i < count; i++) {
			ratioList[index] = type4;
			index++;
		}
	}
	
	public EnemyReference.Type GetRandomEnemy() {
		return (EnemyReference.Type)ratioList[Random.Range(0, 100)];
	}
	
	public static void Init() {
  	listEnemyLevel.Clear();
  	int numRows = 0;	
		string[,] output = CSVReader.GetDataReference("EnemyLevel", out numRows);
  	for (int i = 0; i < numRows; i++) {
  		listEnemyLevel.Add(new EnemyLevelReference(int.Parse(output[i, 0]),
  																							 int.Parse(output[i, 1]),
  																							 float.Parse(output[i, 2]),
  																							 int.Parse(output[i, 3]),
  																							 float.Parse(output[i, 4]),
  																							 int.Parse(output[i, 5]),
  																							 float.Parse(output[i, 6]),
  																							 int.Parse(output[i, 7]),
  																							 float.Parse(output[i, 8])));
  	}
  }
	
	public static EnemyLevelReference Get(int level) {
  	int count = listEnemyLevel.Count;
  	for (int i = 0; i < count; i++) {
  		if (listEnemyLevel[i].level == level) {
  			return listEnemyLevel[i];
  		}
  	}
  	
  	Debug.Log("listEnemyLevel Get Error " + level);
  	return null;
  }
}
