using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubsystemManager : MonoBehaviour
{
    public SubController SubToManage;
    void Start()
    {
        SubToManage = transform.parent.GetComponent<SubController>();
        if (SubToManage == null)
        {
            Debug.LogError("I HAVE NO SUB TO MANAGE");
        }

    }
}
