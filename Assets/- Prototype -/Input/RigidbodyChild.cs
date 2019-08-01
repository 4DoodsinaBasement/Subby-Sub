﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyChild : MonoBehaviour
{
    public Transform parentTransform;
    public Rigidbody myRigidbody, parentRigidbody;

    public float offset = 0;
    
    Vector3 oldRotation, newRotation;
    Vector3 oldPosition, newPosition;
    Vector3 oldVelocity, newVelocity;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        parentRigidbody = parentTransform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (parentTransform != null)
        {
            newRotation = parentTransform.eulerAngles + (transform.eulerAngles - oldRotation);
            transform.eulerAngles = newRotation;
            oldRotation = parentTransform.eulerAngles;

            // newPosition = parentTransform.position + (transform.position - oldPosition);
            // transform.position = newPosition;
            // oldPosition = parentTransform.position;

            newVelocity = parentRigidbody.velocity + (myRigidbody.velocity - oldVelocity);
            myRigidbody.velocity = newVelocity;
            oldVelocity = parentRigidbody.velocity;

            // myRigidbody.velocity = parentRigidbody.velocity;
        }
    }


    float ConvertAngle(float inAngle)
    {
        if (inAngle > 180.0f) { return -(Mathf.Abs(360f - inAngle)); }
        else { return inAngle; }
    }
}
