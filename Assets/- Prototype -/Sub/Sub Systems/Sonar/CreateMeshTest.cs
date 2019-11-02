using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (MeshFilter))]
[RequireComponent(typeof (MeshRenderer))]
public class CreateMeshTest : MonoBehaviour
{
    public Material meshMaterial; 
    
    Vector3[] vertices = new Vector3[12];
    int[] triangles =
    {
        0, 11, 5,
        0, 5, 1,
        0, 1, 7,
        0, 7, 10,
        0, 10, 11,
        1, 5, 9,
        5, 11, 4,
        11, 10, 2,
        10, 7, 6,
        7, 1, 8,
        3, 9, 4,
        3, 4, 2,
        3, 2, 6,
        3, 6, 8,
        3, 8, 9,
        4, 9, 5,
        2, 4, 11,
        6, 2, 10,
        8, 6, 7,
        9, 8, 1
    };
    
    
    void Start()
    {
        LoadVertices();
        
        Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.Optimize();
		mesh.RecalculateNormals();

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = meshMaterial;
    }

    void Update()
    {
        
    }


    void LoadVertices()
    {
        float t = (1.0f + Mathf.Sqrt(5.0f)) / 2.0f;
        
        vertices[0] = new Vector3(-1.0f,  t, 0.0f);
        vertices[1] = new Vector3( 1.0f,  t, 0.0f);
        vertices[2] = new Vector3(-1.0f, -t, 0.0f);
        vertices[3] = new Vector3( 1.0f, -t, 0.0f);
        vertices[4] = new Vector3(0.0f, -1.0f,  t);
        vertices[5] = new Vector3(0.0f,  1.0f,  t);
        vertices[6] = new Vector3(0.0f, -1.0f, -t);
        vertices[7] = new Vector3(0.0f,  1.0f, -t);
        vertices[8] = new Vector3( t, 0.0f, -1.0f);
        vertices[9] = new Vector3( t, 0.0f,  1.0f);
        vertices[10] = new Vector3(-t, 0.0f, -1.0f);
        vertices[11] = new Vector3(-t, 0.0f,  1.0f);
    }
}
