using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    public GameObject sub;
    public float FullLigthDepth;
    public float TotalDarknessDepth;
    Light lt;
   void Start()
    {
        lt = GetComponent<Light>();
    }
       

    // Update is called once per frame
    void Update()
    {
        float depth = sub.transform.position.y;

        if (depth > FullLigthDepth)
        {
            lt.intensity = 1;
        }
        else
        if (depth < TotalDarknessDepth)
        {
            lt.intensity = 0;
        }
        else{
            lt.intensity = (depth- TotalDarknessDepth)/(FullLigthDepth - TotalDarknessDepth);
        }

    
    }
}
