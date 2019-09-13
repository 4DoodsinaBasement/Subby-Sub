using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarTest : MonoBehaviour
{
    Mesh mesh;
    
    public GameObject worldBlip, mapBlip;
    public Transform castOrigin, drawOrigin;
    public int resolution = 100;
    public float range;

    List<GameObject> blips = new List<GameObject>();
    Vector3 lastHitPosition, thisHitPosition;
    
    
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        
        CreateBlips();
        UpdateBlips();
        UpdateLine();
    }

    void FixedUpdate()
    {
    }


    void CreateBlips()
    {
        float angle = 0;
        for (int i = 0; i < resolution; i++)
        {
            float x = Mathf.Sin (angle);
            float z = Mathf.Cos (angle);
            angle += 2 * Mathf.PI / resolution;
        
            Vector3 direction = new Vector3 (x, 0, z);
            Vector3 relativePosition = drawOrigin.position;

            RaycastHit hit;
            if (Physics.Raycast(castOrigin.position, direction, out hit, range, LayerMask.GetMask("Terrain")))
            {
                relativePosition = hit.point - castOrigin.position;
                relativePosition *= 0.01f;
                
                GameObject newBlip = Instantiate(mapBlip, drawOrigin.position + relativePosition, Quaternion.identity);
                blips.Add(newBlip);
                newBlip.transform.parent = drawOrigin.transform;
                newBlip.name = string.Format("Blip {0}", i);
            }
            else
            {
                GameObject newBlip = Instantiate(mapBlip, drawOrigin.position, Quaternion.identity);
                newBlip.SetActive(false);
                blips.Add(newBlip);
                newBlip.transform.parent = drawOrigin.transform;
                newBlip.name = string.Format("Blip {0}", i);
            }
        }
    }

    void UpdateBlips()
    {
        float angle = 0;
        foreach (GameObject blip in blips)
        {
            float x = Mathf.Sin (angle);
            float z = Mathf.Cos (angle);
            angle += 2 * Mathf.PI / resolution;
        
            Vector3 direction = new Vector3 (x, 0, z);
            Vector3 relativePosition = drawOrigin.position;

            RaycastHit hit;
            if (Physics.Raycast(castOrigin.position, direction, out hit, range, LayerMask.GetMask("Terrain")))
            {
                relativePosition = hit.point - castOrigin.position;
                relativePosition *= 0.01f;
                
                blip.SetActive(true);
                blip.transform.position = drawOrigin.position + relativePosition;
            }
            else
            {
                blip.SetActive(false);
            }
        }
    }

    List<GameObject> remappedBlips;
    void UpdateLine()
    {
        int indexOfFirstInactive = -1;
        foreach (GameObject blip in blips)
        {
            if ( !blip.activeSelf )
            {
                indexOfFirstInactive = blips.IndexOf(blip); 
                break;
            }
        }

        if (indexOfFirstInactive != -1)
        {
            remappedBlips = RemapList(blips, indexOfFirstInactive);

            List<List<GameObject>> allLineSegments = new List<List<GameObject>>();
            for (int i = 1; i < remappedBlips.Count; i++) // "i = 1" is on purpose because the list is remapped to the first inactive blip and index 0 doesn't need a check
            {
                int lastIndexChecked = GetNextPosition(i);
                if (i != lastIndexChecked) // If GetNextPosition() returns the same number as passed, it means this blip or the next blip is inactive
                {
                    List<GameObject> currentLineSegment = new List<GameObject>();
                    for (int currentPointInLineSegment = i; currentPointInLineSegment <= lastIndexChecked; currentPointInLineSegment++)
                    {
                        currentLineSegment.Add(remappedBlips[currentPointInLineSegment]);
                    }
                    for (int currentPointInLineSegment = lastIndexChecked; currentPointInLineSegment >= i; currentPointInLineSegment--)
                    {
                        Vector3 newBackBlipPosition = Vector3.Normalize(remappedBlips[currentPointInLineSegment].transform.position - drawOrigin.transform.position) * 0.5f;
                        GameObject newBackBlip = Instantiate(mapBlip, newBackBlipPosition, Quaternion.identity);
                        newBackBlip.transform.parent = remappedBlips[currentPointInLineSegment].transform;
                        newBackBlip.name = remappedBlips[currentPointInLineSegment].name + " back-blip";
                        currentLineSegment.Add(newBackBlip);
                    }
                    allLineSegments.Add(currentLineSegment);
                }
                i = lastIndexChecked;
            }
            
            CreateLineRenderer(allLineSegments);
        }
        else
        {
            // all are active
            // TO-DO: assign remappedBlips to something
        }
    }

    int GetNextPosition(int currentIndex)
    {
        int indexOfLastActive = currentIndex;
        if (currentIndex < remappedBlips.Count - 1)
        {
            if ( remappedBlips[currentIndex].activeSelf && remappedBlips[currentIndex + 1].activeSelf )
            {
                indexOfLastActive = GetNextPosition(currentIndex + 1);
            }
        }
        return indexOfLastActive;
    }

    List<GameObject> RemapList(List<GameObject> copiedList, int startingIndex)
    {
        List<GameObject> listToReturn = new List<GameObject>();

        for (int i = 0; i < copiedList.Count; i++)
        {
            int wrappedIndex;
            
            if (startingIndex + i >= copiedList.Count)
            {
                wrappedIndex = i - copiedList.Count;
            }
            else
            {
                wrappedIndex = i;
            }
            listToReturn.Add(copiedList[startingIndex + wrappedIndex]);
        }

        return listToReturn;
    }

    void CreateLineRenderer(List<List<GameObject>> allLineSegments) // Yes, you read that peramater correctly ;)
    {
        List<Vector3> vertexList = new List<Vector3>();
        List<int> triangleList = new List<int>();
        
        foreach (List<GameObject> lineSegment in allLineSegments)
        {
            Vector3 averagePosition = Vector3.zero;
            foreach (GameObject point in lineSegment)
            {
                vertexList.Add(point.transform.position);
                averagePosition += point.transform.position;
            }

            averagePosition /= lineSegment.Count;
            GameObject averageBlip = Instantiate(mapBlip, averagePosition, Quaternion.identity);
            averageBlip.transform.parent = drawOrigin.transform;
            averageBlip.name = "Average Blip " + allLineSegments.IndexOf(lineSegment);
            vertexList.Add(averageBlip.transform.position);

            // TO-DO: Create Triangles (option #1)
        }

        // TO-DO: Create Triangles (option #2)

        foreach (Vector3 vertex in vertexList)
        {
            Debug.Log(vertex + " at index " + vertexList.IndexOf(vertex));
        }

		// mesh.Clear();
        // TO-DO: Set Vertices
        // To-Do: Set Triangles
		// mesh.Optimize();
		// mesh.RecalculateNormals();
    }
}
