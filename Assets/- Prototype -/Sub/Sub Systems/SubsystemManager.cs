using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubsystemManager : MonoBehaviour
{
	[Tooltip("The axis value will be raised to this power. The higher the value the longer the curve stays at a lower value.")]
	public float axisCurveFactor = 3;

	[ReadOnly] public SubController subToManage;
	[ReadOnly] public SubSystemPHManager subPHMmanager;

	void Start()
	{
		subToManage = transform.parent.GetComponent<SubController>();
		if (subToManage == null)
		{
			Debug.LogError("I HAVE NO SUB TO MANAGE");
		}

		subPHMmanager = transform.parent.GetComponent<SubSystemPHManager>();
		if (subToManage == null)
		{
			Debug.LogError("I HAVE NO SUB PH MANAGER");
		}
	}
}
