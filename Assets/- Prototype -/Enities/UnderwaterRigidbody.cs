using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnderwaterRigidbody : MonoBehaviour
{
	public float buoyancyForce = 5f;
	public float underwaterDrag = 2f;
	public float underwaterAngularDrag = 2f;

	Rigidbody rb;
	Vector3 buoyantVector;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		if (rb != null)
		{
			rb.useGravity = false;
			rb.drag = underwaterDrag;
			rb.angularDrag = underwaterAngularDrag;

			buoyantVector = new Vector3(0, buoyancyForce, 0);
		}
		else
		{
			Debug.LogWarning("Float Script attached to " + gameObject.name + " couldn't find a Rigidbody to Float");
		}
	}

	void Update()
	{
		if (rb != null) { rb.AddForce(buoyantVector, ForceMode.Acceleration); }
	}
}
