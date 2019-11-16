using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager_Sub : HPManager
{
	// public override float currentHP
	// {
	// 	get; set;
	// }

	public override void TakeDamage(float damage)
	{
		// Debug.Log("Sub take damage: " + damage);
		GetComponent<SubSystemPHManager>().AssignDamage(damage);
	}
}
