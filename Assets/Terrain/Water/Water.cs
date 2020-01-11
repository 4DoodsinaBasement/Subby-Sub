using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Water : MonoBehaviour
{
	[Header("Mesh Settings")]
	public int xSize = 10;
	public int zSize = 10;
	public float totalScale = 1;
	Mesh mesh;
	Vector3[] vertices;
	int[] triangles;
	Vector2[] uvs;

	[Header("Debug Settings")]
	public bool drawVerts;
	[Range(2.5f, 10f)] public float drawSize = 5f;

	[ContextMenu("Refresh Settings")]
	void Start()
	{
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
	}

	void Update()
	{
		GenerateMesh();
		UpdateMesh();
	}


	void GenerateMesh()
	{
		vertices = new Vector3[(xSize + 1) * (zSize + 1)];

		for (int i = 0, z = 0; z <= zSize; z++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				vertices[i] = new Vector3(x * totalScale, 0, z * totalScale);
				i++;
			}
		}


		triangles = new int[xSize * zSize * 6];

		int vert = 0;
		int tris = 0;
		for (int z = 0; z < zSize; z++)
		{
			for (int x = 0; x < xSize; x++)
			{
				triangles[tris + 0] = vert + 0;
				triangles[tris + 1] = vert + xSize + 1;
				triangles[tris + 2] = vert + 1;
				triangles[tris + 3] = vert + 1;
				triangles[tris + 4] = vert + xSize + 1;
				triangles[tris + 5] = vert + xSize + 2;

				vert++;
				tris += 6;
			}

			vert++;
		}


		uvs = new Vector2[vertices.Length];

		for (int i = 0; i < uvs.Length; i++)
		{
			uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
		}
	}

	void UpdateMesh()
	{
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateNormals();
	}

	private void OnDrawGizmos()
	{
		if (vertices == null || drawVerts == false) { return; }

		for (int i = 0; i < vertices.Length; i++)
		{
			Gizmos.DrawSphere(vertices[i], (drawSize / 100f));
		}
	}
}
