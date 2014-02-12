using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UntickAutoPlayAnimation : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Component[] animations = gameObject.GetComponentsInChildren<Animation>();
		foreach (Component ani in animations) {
			(ani as Animation).playAutomatically = false;
		}
		
		DestroyImmediate(GetComponent<UntickAutoPlayAnimation>());
	}
}
