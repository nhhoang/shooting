using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityThreading;

public class GameScreen : BaseScreen {
	enum EventId {
		NIL = 0
	}
	
	enum State {
		STATE_INVALID,
		STATE_TRANSITION_IN,
		STATE_READY,
		STATE_PAUSING,
		STATE_PAUSED,
		STATE_RESUMING,
		STATE_TRANSITION_OUT
	}
	
	public static bool isTimePlayMode;  								// Time play mode for 90 seconds, or bullet mode for 100 bullet
	public Controller controller;
	private BulletManager bulletManager;
	private GameScreenUI gameScreenUI;
	private FallingController fallingController;
	
	public override void Init(params object[] inputs) {
    double t = Network.time;
    double t1 = Network.time;
		base.Init(inputs);
		// Init PoolManager
		MyPoolManager.Init();
		// Debug.Log("MyPoolManager.Init load in " + (Network.time - t));
		t = Network.time;
		// Add camera
		child = (GameObject)Instantiate(Resources.Load(commonPrefabPath + "MainCamera"));
		child.name = "MainCamera";
		child.transform.parent = thisTransform;
		screenCamera = child.camera;
//		Debug.Log("MainCamera load in " + (Network.time - t));
		
		// Add UI manager
		gameScreenUI = MyPoolManager.Spawn("GameScreenUI").GetComponent<GameScreenUI>();
		gameScreenUI.transform.parent = thisTransform;
		gameScreenUI.Init();
		
		// Add Controller
		child = (GameObject)Instantiate(Resources.Load(prefabPath + "Controller"));
		child.name = "Controller";
		child.transform.parent = thisTransform;
		controller = child.GetComponent<Controller>();
		
		// Add BulletManager
		bulletManager = new GameObject("Bullet Manager", typeof(BulletManager)).GetComponent<BulletManager>();
		bulletManager.transform.parent = thisTransform;
		bulletManager.Init(controller);
		controller.Init(bulletManager);
		
		isTimePlayMode = true;
		fallingController = gameObject.AddComponent<FallingController>() as FallingController;
		fallingController.Init();
		
		Resources.UnloadUnusedAssets();
		state = (int)State.STATE_READY;
		// Debug.Log("gameScreen load in " + (Network.time - t1));
	}

	public override void DoUpdate(float elapsedSeconds) {
		switch (state) {
			case (int)State.STATE_INVALID:
			break;
			
			case (int)State.STATE_TRANSITION_IN:
			break;
			
			case (int)State.STATE_READY:	
				fallingController.DoUpdate();
			break;
			
			case (int)State.STATE_PAUSING:
			
			break;
			
			case (int)State.STATE_PAUSED:
			
			break;
			
			case (int)State.STATE_RESUMING:
			
			break;
			
			case (int)State.STATE_TRANSITION_OUT:
			
			break;
		}
	}
	
	public override void DoFixedUpdate(float elapsedSeconds) {
		switch (state) {
			case (int)State.STATE_INVALID:
			break;
			
			case (int)State.STATE_TRANSITION_IN:
			break;
			
			case (int)State.STATE_READY:
			break;
			
			case (int)State.STATE_PAUSING:
			
			break;
			
			case (int)State.STATE_PAUSED:
			
			break;
			
			case (int)State.STATE_RESUMING:
			
			break;
			
			case (int)State.STATE_TRANSITION_OUT:
			
			break;
		}
	}
	
	public override bool HandleEvent(int action, params object[] inputs) {
		return true;
	}
	
	public void SetState(int inputState) {
		state = inputState;
		switch (state) {
			case (int)State.STATE_INVALID:
			
			break;
			
			case (int)State.STATE_TRANSITION_IN:
			break;
			
			case (int)State.STATE_READY:
			
			break;
			
			case (int)State.STATE_PAUSING:
			
			break;
			
			case (int)State.STATE_PAUSED:
			
			break;
			
			case (int)State.STATE_RESUMING:
			
			break;
			
			case (int)State.STATE_TRANSITION_OUT:
			
			break;
		}
	}
	
	public override void Destroy() {

	}
	// -- END ARMY CAMP SECTION --
	
	public override void Pause() {}
	public override void Save() {}
	public override void Load() {}
}


// There code below is how to use multithreading
//using UnityEngine;
//using System.Collections;
//using UnityThreading;
//
//public class GameScreen : BaseScreen {
//	enum EventId {
//		NIL = 0
//	}
//	
//	private ScrollerBottomToTop scroller;
//	private EmptyRow emptyRow;
//
//	public override void Prepare(params object[] inputs) {
//		scroller = new GameObject("Scroller", typeof(ScrollerBottomToTop)).GetComponent<ScrollerBottomToTop>();
//		scroller.Init(0.0f, 80.0f, 320.0f, 400.0f);
//		
//		UnityThreadHelper.CreateThread(() =>{
//			return ThreadedYieldCalculation();
//		});
//		
//		Utils.SetParent(thisTransform, scroller.transform);
//	}
//
//	public override void DoUpdate(float elapsedSeconds, UniTouch[] touches, int numTouches) {
//		scroller.DoUpdate(elapsedSeconds, touches, numTouches);
//	}
//	
//	public override bool HandleEvent(int action, params object[] inputs) {
//		return true;
//	}
//	
//	public override void Pause() {}
//	public override void Save() {}
//	public override void Load() {}
//	
//	IEnumerator ThreadedYieldCalculation() {
//		for (int i = 0; i < 500; ++i) {
//		 	System.Threading.Thread.Sleep(1); // simulate calculation
//		
//			yield return new CubeTask(i, scroller, emptyRow, this);
//		}
//	}
//	
//	private class CubeTask : UnityThreading.TaskBase {
//		private int index;
//		private ScrollerBottomToTop scroller;
//		private EmptyRow emptyRow;
//		private BaseScreen baseScreen;
//		private Transform thisTransform;
//		
//		public CubeTask(int index, ScrollerBottomToTop _scroller, EmptyRow _emptyRow, BaseScreen _baseScreen) {
//			this.index = index;
//			scroller = _scroller;
//			emptyRow = _emptyRow;
//			baseScreen = _baseScreen;
//		}
//		
//		protected override IEnumerator Do() {
//			emptyRow = (Instantiate(Resources.Load("Prefabs/Screens/GameScreen/EmptyRow")) as GameObject).AddComponent<EmptyRow>();
//			emptyRow.name = "EmptyRow" + index;
//			emptyRow.Init(emptyRow.renderer.bounds.size.y, baseScreen, (int)GameScreen.EventId.NIL, true);
//			scroller.AddRow(emptyRow);
//			
//			return null;
//		}
//	}
//}
