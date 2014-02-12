using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	public Transform gunTrans;
	private Transform trans;
	private Vector3 gunPos;
	private BulletManager bulletManager;
	
	void Awake() {
		trans = transform;
		gunTrans = trans.Find("Gun");
		gunPos = gunTrans.position;
	}
	
	public void Init(BulletManager bulletManager) {
		this.bulletManager = bulletManager;
		gunTrans.GetComponent<UIAnchor>().uiCamera = Director.Instance.currentScreen.screenCamera;
	}
	
	// Subscribe to events
	void OnEnable() {
		EasyTouch.On_TouchStart += On_TouchStart;
		EasyTouch.On_TouchDown += On_TouchDown;
		EasyTouch.On_TouchUp += On_TouchUp;
	}

	void OnDisable() {
		UnsubscribeEvent();
	}
	
	void OnDestroy() {
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent(){
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_TouchDown -= On_TouchDown;
		EasyTouch.On_TouchUp -= On_TouchUp;
	}
	
	public void On_TouchStart(Gesture gesture){
	}
	
	// During the touch is down
	public void On_TouchDown(Gesture gesture){
	}
	
	// At the touch end
	public void On_TouchUp(Gesture gesture){
		bulletManager.Fire(gesture.GetTouchToWordlPoint(0, true) - gunPos);
	}
}
