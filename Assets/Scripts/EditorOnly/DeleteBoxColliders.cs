using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DeleteBoxColliders : MonoBehaviour {
	// Use this for initialization
	void Start() {
		Component[] renderers = gameObject.GetComponentsInChildren<BoxCollider>();
		foreach (Component render in renderers) {
			DestroyImmediate(render as BoxCollider);
		}
	}
}