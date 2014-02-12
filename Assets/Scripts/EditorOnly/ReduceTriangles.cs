using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class ReduceTriangles : MonoBehaviour {
	// Use this for initialization
	void Start() {
		// Drag this script to plane which must have rotation is (90, 0, 0);
		Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
		mesh.Clear();
		
		// Vertices.
		Vector3[] vertices = new Vector3[4];
		float maxX = (renderer.bounds.max.x - transform.position.x) / transform.localScale.x;
		float minX = (renderer.bounds.min.x - transform.position.x) / transform.localScale.x;
		float currentY = (transform.position.z - transform.position.z) / transform.localScale.y;
		float maxZ = -(renderer.bounds.max.y - transform.position.y) / transform.localScale.z;
		float minZ = -(renderer.bounds.min.y - transform.position.y) / transform.localScale.z;
		vertices[0] = new Vector3(maxX, currentY, maxZ);
		vertices[1] = new Vector3(maxX, currentY, minZ);
		vertices[2] = new Vector3(minX, currentY, minZ);
		vertices[3] = new Vector3(minX, currentY, maxZ);
		mesh.vertices = vertices;
		
		// Triangles.
		int[] triangles = new int[6];
		triangles[0] = 0;
		triangles[1] = 3;
		triangles[2] = 1;
		triangles[3] = 3;
		triangles[4] = 2;
		triangles[5] = 1;
		mesh.triangles = triangles;
		
		// UV - Coordinates.
		// 0 __ 3
		// |		|
		// 1 __ 2
		// 1-0-3-2
		Vector2[] coordinates = new Vector2[4];
		coordinates[0] = new Vector2(0, 1);
		coordinates[1] = new Vector2(0, 0);
		coordinates[2] = new Vector2(1, 0);
		coordinates[3] = new Vector2(1, 1);
		mesh.uv = coordinates;
		
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}
}
