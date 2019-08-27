using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseMeshTest : MonoBehaviour
{
    public Mesh templateMesh;
    
    
    void Start()
    {
        if (templateMesh == null) { Debug.LogError("No mesh provided for Sonar Scanning!"); }
    }

    void Update()
    {
        
    }


    void CreateBlips()
    {
        
    }
}
