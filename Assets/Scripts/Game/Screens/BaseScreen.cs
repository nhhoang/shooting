using UnityEngine;
using System.Collections;

public class BaseScreen : MonoBehaviour {
	protected GameObject row;
	protected GameObject child;
	protected Transform thisTransform;
	protected int state = 0;
	protected string prefabPath = "Prefabs/";
	protected string commonPrefabPath = "Prefabs/";
	protected string uiPrefabPath = "Prefabs/";
	protected Director director;
	protected bool isActionValid = true;
	public Camera screenCamera;
	public Director.ScreenType screenType;
	
	public virtual void Awake() {
		thisTransform = transform;
		Application.runInBackground = true;
	}
	
	public virtual void Init(params object[] inputs) {
		director = Director.Instance;
		prefabPath = "Prefabs/Screens/" + name + "/";
		commonPrefabPath = "Prefabs/Screens/Common/";
		uiPrefabPath = "Prefabs/Screens/UI/";
	}
	
	public virtual void DoUpdate(float elapsedSeconds) {}
	public virtual void DoFixedUpdate(float elapsedSeconds) {}
	public virtual void DoLateUpdate(float elapsedSeconds) {}
	public virtual void Destroy(){}
	public virtual bool HandleEvent(int action, params object[] inputs) {
		return false;
	}
	public string PrefabPath {
		get {return prefabPath;}
	}
	public string UIPrefabPath {
		get {return uiPrefabPath;}
	}
	public Transform ThisTransform {
		get {return thisTransform;}
	}
	
	public virtual void Pause() {}
	public virtual void Save() {}
	public virtual void Load() {}
}
