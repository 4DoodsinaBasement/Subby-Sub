using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
	public int baseDamage = 7;
	public float blastRadius;
	public float blastForce;


	void Start()
	{

	}

	void Update()
	{

	}

	void OnCollisionEnter(Collision other)
	{
		// HPManager somePoorChap = other.collider.gameObject.GetComponent<HPManager>();

		// if (somePoorChap != null)
		// {
		// 	somePoorChap.currentHP -= baseDamage;
		// 	Destroy(gameObject);
		// }

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, blastRadius);

		List<HPManager> hitEntities = new List<HPManager>();
		List<Rigidbody> hitRigidbodies = new List<Rigidbody>();

		foreach (Collider hit in hitColliders)
		{
			HPManager hitEntity = hit.GetComponent<HPManager>();
			Rigidbody hitRigidbody = hit.GetComponent<Rigidbody>();

			if (hitEntity != null) { hitEntities.Add(hitEntity); }
			if (hitRigidbody != null) { hitRigidbodies.Add(hitRigidbody); }
		}

		foreach (HPManager entity in hitEntities)
		{
			entity.TakeDamage(baseDamage);
		}

		foreach (Rigidbody rb in hitRigidbodies)
		{
			rb.AddExplosionForce(blastForce, transform.position, blastRadius, 0f, ForceMode.VelocityChange);
		}
	}

	void OnDrawGizmosSelected()
	{
		// Draw a yellow sphere at the transform's position
		Gizmos.color = new Color32(240, 200, 0, 255);
		Gizmos.DrawWireSphere(transform.position, blastRadius);
		Gizmos.color = new Color32(240, 200, 0, 100);
		Gizmos.DrawSphere(transform.position, blastRadius);
	}
}
