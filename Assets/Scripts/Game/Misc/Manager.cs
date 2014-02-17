// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using Holoville.HOTween;
using System.Collections;

public class Manager : MonoBehaviour {
	private static bool isInitData = false;
	private static Manager instance;
	
	void Awake() {
		HOTween.Init(true, true, true);

		if (!isInitData) {
			isInitData = true;
			instance = this;
			Init();
		}
	}
	
	public void Init() {
		// Load Game data
		GameData.LoadData();
	}
	
	void OnDestroy() {
		// Reset all static variables in game
	}
	
	public void Test() {
		Debug.Log("test");
	}
}