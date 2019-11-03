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
		HPManager somePoorChap = other.gameObject.GetComponent<HPManager>();

		if (somePoorChap != null)
		{
			somePoorChap.currentHP -= baseDamage;
			Destroy(gameObject);
		}
	}
}
