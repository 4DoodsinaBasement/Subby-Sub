using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
	public int baseDamage = 7;
	void Start()
	{

	}

	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		SubController somePoorChap = other.gameObject.GetComponent<SubController>();

		if (somePoorChap != null)
		{
			somePoorChap.ModifyHealth(-baseDamage);
			Destroy(gameObject);
		}
	}
}
