using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	public Transform centerPoint;
	public float radius;
	public GameObject prefab;
	
	// Use this for initialization
	void Start () {
		Vector3 v;
		for (int i = 0; i < 180; i++) {
			v = new Vector3(radius * Mathf.Cos(i * Mathf.Deg2Rad) + centerPoint.position.x, radius * Mathf.Sin(i * Mathf.Deg2Rad) + centerPoint.position.y, 0.0f);
			GameObject go = Instantiate(prefab) as GameObject;
			go.transform.position = v;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
