using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public int totalPoint = 0;
	public int currentLevel = 1;
	private int[] levelPoints = {0, 1000, 3000, 6000, 10000, 20000, 30000, 45000};
	
	public static LevelManager Instance;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	public void AddPoint(int point) {
		totalPoint += point;
		GameScreenUI.Instance.SetScore(totalPoint.ToString());
		if (totalPoint > levelPoints[currentLevel]) {
			// LevelUp
			
		}
	}
	
	void LevelUp() {
		currentLevel++;
		totalPoint = 0;
	}
}
