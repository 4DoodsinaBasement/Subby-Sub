using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubsystemManager : MonoBehaviour
{
    [Tooltip("The axis value will be raised to this power. The higher the value the longer the curve stays at a lower value.")]
    [Range(0.5f, 5f)]
    public float axisCurveFactor;
    
    [ReadOnly] public SubController SubToManage;
    void Start()
    {
        SubToManage = transform.parent.GetComponent<SubController>();
        if (SubToManage == null)
        {
            Debug.LogError("I HAVE NO SUB TO MANAGE");
        }

    }
}
