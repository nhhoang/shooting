using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DeleteRenderers : MonoBehaviour {
	// Use this for initialization
	void Start() {
		Component[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Component render in renderers) {
			if (render != null) {
				DestroyImmediate(render.gameObject);
			}
		}
	}
}
