using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightDirection : MonoBehaviour
{
    Vector3 lightDirection;

    void Start()
    {
        lightDirection = transform.forward;
        Debug.Log(string.Format("Light Direction: X={0}, Y={1}, Z={2}", lightDirection.x, lightDirection.y, lightDirection.z));
    }

    void Update()
    {
        
    }
}
