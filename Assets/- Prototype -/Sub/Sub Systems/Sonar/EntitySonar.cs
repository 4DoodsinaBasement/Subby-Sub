using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SonarEntityType { Friendly, Neutral, Hostile };

public class EntitySonar : MonoBehaviour
{
	[Header("Render Settings")]
	public GameObject castOrigin;
	public GameObject drawOrigin;
	public GameObject sonarGlobe;
	public GameObject entityBlipPrefab;
	public Material friendlyMaterial;
	public Material neutralMaterial;
	public Material hostileMaterial;

	[Header("Sonar Settings")]
	public float sonarRange = 200f;
	public List<EntitySonar_Blip> blips = new List<EntitySonar_Blip>();


	void Start()
	{
		SonarRenderer[] worldEntities = FindObjectsOfType<SonarRenderer>();

		foreach (SonarRenderer worldEntity in worldEntities)
		{
			GameObject newMapEntity = Instantiate(entityBlipPrefab, drawOrigin.transform);
			blips.Add(new EntitySonar_Blip(worldEntity, newMapEntity));
		}

		RecalculateBlips();
	}

	void Update()
	{
		RecalculateBlips();
	}


	void RecalculateBlips()
	{
		Vector3 localSize = (sonarGlobe.transform.localScale / 2) / sonarRange;

		List<EntitySonar_Blip> blipsToRemove = new List<EntitySonar_Blip>();
		foreach (EntitySonar_Blip blip in blips)
		{
			if (blip.worldObject == null)
			{
				blipsToRemove.Add(blip);
			}
			else
			{
				if (Vector3.Distance(blip.worldObject.transform.position, castOrigin.transform.position) < sonarRange)
				{
					blip.mapObject.SetActive(true);

					Vector3 relativePosition = blip.worldObject.transform.position - castOrigin.transform.position;

					Vector3 relativeScaleDownPosition = new Vector3(
						relativePosition.x * localSize.x,
						relativePosition.y * localSize.y,
						relativePosition.z * localSize.z
					);

					blip.mapObject.transform.position = drawOrigin.transform.position + relativeScaleDownPosition;
				}
				else
				{
					blip.mapObject.SetActive(false);
				}

				switch (blip.worldObject.entityType)
				{
					case SonarEntityType.Friendly:
						blip.mapObject.GetComponent<MeshRenderer>().material = friendlyMaterial;
						break;

					case SonarEntityType.Neutral:
						blip.mapObject.GetComponent<MeshRenderer>().material = neutralMaterial;
						break;

					case SonarEntityType.Hostile:
						blip.mapObject.GetComponent<MeshRenderer>().material = hostileMaterial;
						break;
				}
			}
		}

		foreach (EntitySonar_Blip blipToRemove in blipsToRemove)
		{
			Destroy(blipToRemove.mapObject);
			blips.Remove(blipToRemove);
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
