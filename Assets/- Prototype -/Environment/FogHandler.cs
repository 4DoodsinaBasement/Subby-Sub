using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogHandler : MonoBehaviour
{
	[Header("Generation Settings")]
	public GameObject fogCubePrefab;
	public float terrainSize = 1000;
	public int fogLayers = 100;

	[Header("Generated Cubes")]
	public List<GameObject> fogCubes = new List<GameObject>();

	[ContextMenu("Generate Fog Cubes")]
	void GenerateCubes()
	{
		if (fogCubes.Count > 0)
			foreach (GameObject cube in fogCubes)
			{
				Destroy(cube);
			}
		fogCubes.Clear();

		for (int i = 0; i < fogLayers; i++)
		{
			GameObject newFogCube = Instantiate(fogCubePrefab, transform);
			newFogCube.name = "Fog Cube (h" + i + ")";
			fogCubes.Add(newFogCube);

			newFogCube.transform.localScale = new Vector3(terrainSize, 1, terrainSize);

			Vector3 newPosition = newFogCube.transform.localPosition;
			newPosition.y = i;
			newFogCube.transform.localPosition = newPosition;
		}
	}
}
