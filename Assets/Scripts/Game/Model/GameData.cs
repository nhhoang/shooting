using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {
	private static int currentScore;
	private static int bestScore;
	
	public static void UpdateScore(int gainPoints) {
		currentScore += gainPoints;
	}
	
	public static void UpdateBestScore() {
		if (bestScore < currentScore) {
			bestScore = currentScore;
		}
	}
	// -------------END SAVE AND LOAD GAME -------------------
}
