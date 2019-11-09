using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntitySonar : MonoBehaviour
{
	public float sonarRange = 200f;

	public List<SonarRenderer> sonarEntities = new List<SonarRenderer>();

	void Start()
	{
		sonarEntities = FindObjectsOfType<SonarRenderer>().ToList();
	}

	void Update()
	{

	}
}
