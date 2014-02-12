using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UntickCastShadow : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Component[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Component render in renderers) {
			render.renderer.receiveShadows = false;
			render.renderer.castShadows = false;
		}
		
		DestroyImmediate(GetComponent<UntickCastShadow>());
	}
}
