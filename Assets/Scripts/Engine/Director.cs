using UnityEngine;
using System.Collections.Generic;

public class Director : MonoBehaviour {
	public enum ScreenType {
		SPLASH = 0,
		GAME
	}
	
	public ScreenType startScreen;
	
	[HideInInspector]
	public BaseScreen currentScreen;
	[HideInInspector]
	private Transform thisTransform;
	private Transform container;
	public ScreenType currentScreenId = 0;
	private ScreenType previousId = 0;
	private float elapsedSeconds;
	private float totalElapsedSecond = 0.0f;
	private bool isGameStartNormally = true;
	
	public static Director Instance { get; private set; }
	
	public void Awake() {
		Instance = this;
		isGameStartNormally = true;
		thisTransform = transform;
		container = new GameObject("Container").transform;
		container.position = Vector3.zero;
		container.parent = thisTransform;
		container.transform.localScale += new Vector3(0.0f, 0.0f, 0.001f);

		SetScreen(startScreen);
	}
	
	void Update() {
		elapsedSeconds = Time.deltaTime > 0.05f ? 0.05f : Time.deltaTime;
		totalElapsedSecond += elapsedSeconds;
		currentScreen.DoUpdate(elapsedSeconds);
	}
	
	void LateUpdate() {
		currentScreen.DoLateUpdate(elapsedSeconds);
	}
	
	// Process when player exist and resume game
	void OnApplicationPause(bool Pause) {
		if (Pause && Manager.gameData != null) {

		} else if (!Pause && !isGameStartNormally && !(Application.platform == RuntimePlatform.OSXEditor)) {

		}
	}

	void OnApplicationQuit() {

	}
	
	public void SetScreen(ScreenType screenId, params object[] inputs) {
		previousId = currentScreenId;
		currentScreenId = screenId;
		if (currentScreen) {
			currentScreen.Destroy();
		}
		
		Reset();
		
		switch (currentScreenId) {
			case ScreenType.GAME:
				currentScreen = new GameObject("GameScreen", typeof(GameScreen)).GetComponent<GameScreen>();				
			break;
		}
		
		currentScreen.name = currentScreen.GetType().ToString();
		currentScreen.Init(inputs);
		currentScreen.transform.parent = container;
	}
	
	public void Pause() {
		currentScreen.Pause();
	}
	
	public void Save() {
		currentScreen.Save();
	}
	
	public void Reset() {
		if (container && container.gameObject != null) {
			Destroy(container.gameObject);
		}
		
		container = new GameObject("Container").transform;
		container.position = Vector3.zero;
		container.parent = thisTransform;
		container.transform.localScale += new Vector3(0.0f, 0.0f, 0.001f);
	}
	
	public BaseScreen CurrentScreen {
		get {return currentScreen;}
	}
	
	public float ElapsedSeconds {
		get {return elapsedSeconds;}
	}
}
