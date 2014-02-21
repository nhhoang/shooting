using UnityEngine;
using System;
using Boomlagoon.JSON;

public class GameData {
	private static int currentScore;
	private static int bestTimeModeScore;
	private static int bestBulletModeScore;
	
	private static int isSoundOn = 1;
	private static int isMusicOn = 1;
	private static int isFreeApps = 1;
	
	public static void InitGame() {
		currentScore = 0;
	}
	
	public static void UpdateScore(int gainPoints) {
		currentScore += gainPoints;
		UpdateBestScore();
		Debug.Log ("currentScore ------- " + currentScore);
	}
	
	public static void UpdateBestScore() {
		if (GameScreen.isTimePlayMode) {
			if (bestTimeModeScore < currentScore) {
				bestTimeModeScore = currentScore;
			}
		} else {
			if (bestBulletModeScore < currentScore) {
				bestBulletModeScore = currentScore;
			}
		}
	}

	public static int GetCurrentScore() {
		return currentScore;
	}

	public static int GetBestScore() {
		return GameScreen.isTimePlayMode ? bestTimeModeScore : bestBulletModeScore;
	}

	public static void PaidForApps() {
		isFreeApps = 0;
	}

	private static string fileName = "MIData.txt";
	private static string secureKey = "aloha@";
		
	// -------------SAVE AND LOAD GAME -------------------
	public static void SaveData() {
		string dataStr = "";
		JSONObject j = new JSONObject();
		j.Add("tScore", bestTimeModeScore);
		j.Add("bScore", bestBulletModeScore);
		
		j.Add("music", isMusicOn);
		j.Add("sound", isSoundOn);
		j.Add("notPaid", isFreeApps);
		
		dataStr = j.ToString();
		string md5 = Utils.Md5Sum(secureKey + dataStr);
		TextFileManager.SaveToFile(fileName, LZWCompression.Compress(dataStr + "@" + md5), false);
	}
	
	public static void LoadData() {
		string dataStr = TextFileManager.LoadFromFile(fileName);
		if (dataStr != "") {
			try {
				dataStr = LZWCompression.Decompress(dataStr);
				string[] result = dataStr.Split('@');
				if (result.Length > 1) {
					dataStr = result[0];
					string md5 = result[1];
					
					if (md5 == Utils.Md5Sum(secureKey + dataStr)) {
						// Load data
						JSONObject j = JSONObject.Parse(dataStr);
						
						bestTimeModeScore = j.GetInt("tScore");
						bestBulletModeScore = j.GetInt("bScore");
						isMusicOn = j.GetInt("music");
						isSoundOn = j.GetInt("sound");
						isFreeApps = j.GetInt("notPaid");
					} else {
						Debug.Log("original file is modified");
					}
					
					Debug.Log("load data " + dataStr);
				}
			} catch(Exception e) {
				Debug.Log("original file is modified");
			}
		}
	}
	// -------------END SAVE AND LOAD GAME -------------------
}