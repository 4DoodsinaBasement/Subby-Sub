using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    public GameObject sub;
    Light lt;
   void Start()
    {
        lt = GetComponent<Light>();
    }
       

    // Update is called once per frame
    void Update()
    {
        float depth = sub.transform.position.y;

        if (depth >0)
        {
            lt.intensity = 1;
        }
        else
        if (depth < -100)
        {
            lt.intensity = 0;
        }
        else{
            lt.intensity = (depth * -1)/100;
        }

    
    }
}
