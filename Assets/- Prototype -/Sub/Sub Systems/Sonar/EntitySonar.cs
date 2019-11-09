using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SonarEntityType { Friendly, Neutral, Hostile };

public class EntitySonar : MonoBehaviour
{
	[Header("Sonar Settings")]
	public float sonarRange = 200f;

	[Header("Render Settings")]
	public GameObject castOrigin;
	public GameObject sonarGlobe;
	public GameObject entityBlipPrefab;
	public Material friendlyMaterial;
	public Material neutralMaterial;
	public Material hostileMaterial;

	public List<EntitySonar_Blip> blips = new List<EntitySonar_Blip>();

	void Start()
	{
		SonarRenderer[] worldEntities = FindObjectsOfType<SonarRenderer>();

		foreach (SonarRenderer worldEntity in worldEntities)
		{
			GameObject newMapEntity = Instantiate(entityBlipPrefab, transform);

			switch (worldEntity.entityType)
			{
				case SonarEntityType.Friendly:
					newMapEntity.GetComponent<MeshRenderer>().material = friendlyMaterial;
					break;

				case SonarEntityType.Neutral:
					newMapEntity.GetComponent<MeshRenderer>().material = neutralMaterial;
					break;

				case SonarEntityType.Hostile:
					newMapEntity.GetComponent<MeshRenderer>().material = hostileMaterial;
					break;
			}

			blips.Add(new EntitySonar_Blip(worldEntity, newMapEntity));
		}

		RepositionBlips();
	}

	void Update()
	{
		RepositionBlips();
	}

	void RepositionBlips()
	{
		Vector3 localSize = (sonarGlobe.transform.localScale / 2) / sonarRange;

		foreach (EntitySonar_Blip blip in blips)
		{
			if (Vector3.Distance(blip.worldObject.transform.position, castOrigin.transform.position) < sonarRange)
			{
				blip.mapObject.SetActive(true);

				Vector3 relativePosition = blip.worldObject.transform.position - castOrigin.transform.position;

				Vector3 newPosition = new Vector3(
					relativePosition.x * localSize.x,
					relativePosition.y * localSize.y,
					relativePosition.z * localSize.z
				);

				blip.mapObject.transform.localPosition = newPosition;
			}
			else
			{
				blip.mapObject.SetActive(false);
			}
		}
	}
}

public struct EntitySonar_Blip
{
	public SonarRenderer worldObject;
	public GameObject mapObject;

	public EntitySonar_Blip(SonarRenderer worldObject, GameObject mapObject)
	{
		this.worldObject = worldObject;
		this.mapObject = mapObject;
	}
}
