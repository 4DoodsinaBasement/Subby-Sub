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
	public GameObject subIndicator;
	[Space(10f)]
	public GameObject terrainMeshPrefab;
	public Material terrainMaterial;
	[Space(10f)]
	public GameObject entityBlipPrefab;
	public Material friendlyMaterial;
	public Material neutralMaterial;
	public Material hostileMaterial;

	[Header("Sonar Settings")]
	public float sonarRange = 200f;
	public int terrainResolution = 25;
	public float refreshRate = 1f;
	float nextRefresh = 0;
	[HideInInspector] public List<EntitySonar_EntityBlip> entities = new List<EntitySonar_EntityBlip>();
	[HideInInspector] public List<EntitySonar_TerrainBlip> terrainVerts = new List<EntitySonar_TerrainBlip>();
	[HideInInspector] public List<EntitySonar_TerrainMesh> terrainMeshes = new List<EntitySonar_TerrainMesh>();


	void Start()
	{
		GenerateTerrainMeshes();
		GenerateEntityBlips();

		nextRefresh = Time.time + refreshRate;

		UpdateSubIndicator();
		UpdateTerrainMeshes();
		UpdateEntityBlips();
	}

	void Update()
	{
		if (refreshRate > 0)
		{
			if (Time.time >= nextRefresh)
			{
				UpdateSubIndicator();
				UpdateTerrainMeshes();
				UpdateEntityBlips();
				nextRefresh = Time.time + refreshRate;
			}
		}
		else
		{
			UpdateSubIndicator();
			UpdateTerrainMeshes();
			UpdateEntityBlips();
		}
	}


	void UpdateSubIndicator()
	{
		Vector3 newSize = subIndicator.transform.localScale;
		newSize.x = 1f / sonarRange;
		newSize.y = 1f / sonarRange;
		newSize.z = 1f / sonarRange;
		subIndicator.transform.localScale = newSize;
	}

	void GenerateTerrainMeshes()
	{
		Terrain[] worldTerrain = FindObjectsOfType<Terrain>();
		foreach (Terrain thisTerrain in worldTerrain)
		{
			GameObject newMeshObject = Instantiate(terrainMeshPrefab, drawOrigin.transform.Find("Terrain"));
			newMeshObject.name = "Blip: " + thisTerrain.name;
			newMeshObject.GetComponent<MeshRenderer>().material = terrainMaterial;

			EntitySonar_TerrainMesh newTerrainMesh = new EntitySonar_TerrainMesh(newMeshObject, (1000 / terrainResolution), (1000 / terrainResolution));
			terrainMeshes.Add(newTerrainMesh);

			for (int x = -500; x < 500; x += terrainResolution)
			{
				for (int z = -500; z < 500; z += terrainResolution)
				{
					float positionHeight = thisTerrain.SampleHeight(new Vector3(x, 0, z));

					newTerrainMesh.AddPosition(new Vector3(x, positionHeight, z), Vector3.zero);

					// GameObject newMapPosition = Instantiate(entityBlipPrefab, drawOrigin.transform.Find("Terrain"));
					// newMapPosition.name = "Blip: " + newWorldPosition.ToString();
					// newMapPosition.GetComponent<MeshRenderer>().material = neutralMaterial;
					// terrainVerts.Add(new EntitySonar_TerrainBlip(newWorldPosition, newMapPosition));
				}
			}
		}
	}

	void UpdateTerrainMeshes()
	{
		Vector3 localSize = (sonarGlobe.transform.localScale / 2) / sonarRange;

		foreach (EntitySonar_TerrainMesh mesh in terrainMeshes)
		{
			foreach (Vector3 worldPosition in mesh.worldPositions)
			{
				if (Vector3.Distance(worldPosition, castOrigin.transform.position) < (sonarRange + terrainResolution))
				{
					Vector3 relativePosition = worldPosition - castOrigin.transform.position;

					Vector3 relativeScaleDownPosition = new Vector3(
						relativePosition.x * localSize.x,
						relativePosition.y * localSize.y,
						relativePosition.z * localSize.z
					);

					mesh.SetMapPosition(worldPosition, /* drawOrigin.transform.position +  */relativeScaleDownPosition);
				}
				else
				{
					mesh.SetMapPosition(worldPosition, new Vector3(0, -((sonarGlobe.transform.localScale.y / 2) + 0.5f), 0));
				}
			}

			// foreach (Vector3 mapPosition in mesh.mapPositions)
			// {
			// 	GameObject newMapPosition = Instantiate(entityBlipPrefab, drawOrigin.transform.position + mapPosition, Quaternion.identity, mesh.meshObject.transform);
			// 	// newMapPosition.name = "Blip: " + .ToString();
			// 	newMapPosition.GetComponent<MeshRenderer>().material = neutralMaterial;
			// }

			mesh.GenerateMesh();
			Vector3 newRotation = mesh.meshObject.transform.eulerAngles;
			newRotation.y = -castOrigin.transform.eulerAngles.y;
			mesh.meshObject.transform.eulerAngles = newRotation;
		}

		// foreach (EntitySonar_TerrainBlip blip in terrainVerts)
		// {
		// 	float distance = Vector3.Distance(blip.worldPosition, castOrigin.transform.position);
		// 	if (distance < sonarRange)
		// 	{
		// 		blip.mapPosition.SetActive(true);

		// 		Vector3 relativePosition = blip.worldPosition - castOrigin.transform.position;

		// 		Vector3 relativeScaleDownPosition = new Vector3(
		// 			relativePosition.x * localSize.x,
		// 			relativePosition.y * localSize.y,
		// 			relativePosition.z * localSize.z
		// 		);

		// 		blip.mapPosition.transform.position = drawOrigin.transform.position + relativeScaleDownPosition;
		// 	}
		// 	else
		// 	{
		// 		blip.mapPosition.SetActive(false);
		// 	}
		// }
	}

	void GenerateEntityBlips()
	{
		SonarRenderer[] worldEntities = FindObjectsOfType<SonarRenderer>();
		foreach (SonarRenderer worldEntity in worldEntities)
		{
			GameObject newMapEntity = Instantiate(entityBlipPrefab, drawOrigin.transform.Find("Entities"));
			newMapEntity.name = "Blip: " + worldEntity.name;
			entities.Add(new EntitySonar_EntityBlip(worldEntity, newMapEntity));
		}
	}

	void UpdateEntityBlips()
	{
		Vector3 localSize = (sonarGlobe.transform.localScale / 2) / sonarRange;

		List<EntitySonar_EntityBlip> blipsToRemove = new List<EntitySonar_EntityBlip>();
		foreach (EntitySonar_EntityBlip blip in entities)
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

		foreach (EntitySonar_EntityBlip blipToRemove in blipsToRemove)
		{
			Destroy(blipToRemove.mapObject);
			entities.Remove(blipToRemove);
		}
	}
}

public struct EntitySonar_TerrainBlip
{
	public Vector3 worldPosition;
	public GameObject mapPosition;

	public EntitySonar_TerrainBlip(Vector3 worldPosition, GameObject mapPosition)
	{
		this.worldPosition = worldPosition;
		this.mapPosition = mapPosition;
	}
}

public class EntitySonar_TerrainMesh
{
	public GameObject meshObject;
	public int xSize, zSize;
	public List<Vector3> worldPositions = new List<Vector3>();
	public List<Vector3> mapPositions = new List<Vector3>();
	int[] mapTriangles;

	public EntitySonar_TerrainMesh(GameObject meshObject, int xSize, int zSize)
	{
		this.meshObject = meshObject;
		this.xSize = xSize;
		this.zSize = zSize;

		mapTriangles = new int[xSize * zSize * 6];

		int vertIndex = 0, triIndex = 0;
		for (int z = 0; z < zSize - 1; z++)
		{
			for (int x = 0; x < xSize - 1; x++)
			{
				mapTriangles[0 + triIndex] = vertIndex + 0;
				mapTriangles[1 + triIndex] = vertIndex + 1;
				mapTriangles[2 + triIndex] = vertIndex + xSize + 0;
				mapTriangles[3 + triIndex] = vertIndex + 1;
				mapTriangles[4 + triIndex] = vertIndex + xSize + 1;
				mapTriangles[5 + triIndex] = vertIndex + xSize + 0;

				vertIndex++;
				triIndex += 6;
			}
			vertIndex++;
		}
	}

	public void AddPosition(Vector3 newWorldPosition, Vector3 newMapPosition)
	{
		worldPositions.Add(newWorldPosition);
		mapPositions.Add(newMapPosition);
	}

	public void SetMapPosition(Vector3 referenceWorldPosition, Vector3 newMapPosition)
	{
		int index = worldPositions.IndexOf(referenceWorldPosition);
		mapPositions[index] = newMapPosition;
	}

	public void GenerateMesh()
	{
		Mesh meshToRender = meshObject.GetComponent<MeshFilter>().mesh;
		meshToRender.Clear();
		meshToRender.SetVertices(mapPositions);
		meshToRender.SetTriangles(mapTriangles, 0);
		meshToRender.RecalculateNormals();
	}
}

public struct EntitySonar_EntityBlip
{
	public SonarRenderer worldObject;
	public GameObject mapObject;

	public EntitySonar_EntityBlip(SonarRenderer worldObject, GameObject mapObject)
	{
		this.worldObject = worldObject;
		this.mapObject = mapObject;
	}
}
