using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
	public bool destroyOnDeath = true;
	public float maxHP;

	[SerializeField]
	[ReadOnly]
	private float _currentHP; public virtual float currentHP
	{
		get { return _currentHP; }
		set { _currentHP = value; if (_currentHP > maxHP) { _currentHP = maxHP; } }
	}

	public virtual void TakeDamage(float damage)
	{
		currentHP -= damage;
	}


	void Start()
	{
		if (maxHP <= 0)
		{
			Debug.LogWarning("Max HP for " + gameObject.name + " initialized to 0. Setting it to 1 instead.");
			maxHP = 1;
		}
		currentHP = maxHP;
	}

	void Update()
	{
		if (currentHP <= 0 && destroyOnDeath) { Destroy(gameObject); }
	}
}
