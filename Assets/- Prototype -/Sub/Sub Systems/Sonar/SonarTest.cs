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
    }

    void FixedUpdate()
    {
        UpdateBlips();
        UpdateLine();
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

            List<List<Vector3>> allLineSegments = new List<List<Vector3>>();
            for (int i = 1; i < remappedBlips.Count; i++) // "i = 1" is on purpose because the list is remapped to the first inactive blip and index 0 doesn't need a check
            {
                int lastIndexChecked = GetNextPosition(i);
                if (i != lastIndexChecked) // If GetNextPosition() returns the same number as passed, it means this blip or the next blip is inactive
                {
                    List<Vector3> currentLineSegment = new List<Vector3>();
                    for (int currentPointInLineSegment = i; currentPointInLineSegment <= lastIndexChecked; currentPointInLineSegment++)
                    {
                        // Right now this system depends on GameObjects using .localPosition
                        // If we want to convert this over to Blips and Vector3s, then we'll need to fix the "only works at 0,0,0" bug by calcualting localPosition maually
                        currentLineSegment.Add(remappedBlips[currentPointInLineSegment].transform.localPosition); 
                    }
                    for (int currentPointInLineSegment = lastIndexChecked; currentPointInLineSegment >= i; currentPointInLineSegment--)
                    {
                        Vector3 newBackBlipPosition = Vector3.Normalize(remappedBlips[currentPointInLineSegment].transform.position - drawOrigin.transform.position) * 0.5f;
                        currentLineSegment.Add(newBackBlipPosition);
                    }
                    allLineSegments.Add(currentLineSegment);
                }
                i = lastIndexChecked;
            }
            
            GenerateMesh(allLineSegments);
        }
        else
        {
            // all are active
            // TO-DO: assign remappedBlips to something
        }
    }

    int GetNextPosition(int currentIndex) // Recursive function to iterate through all of the blips
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
            
            if (startingIndex + i >= copiedList.Count) { wrappedIndex = i - copiedList.Count; }
            else { wrappedIndex = i; }
            listToReturn.Add(copiedList[startingIndex + wrappedIndex]);
        }

        return listToReturn;
    }

    void GenerateMesh(List<List<Vector3>> allLineSegments) // Yes, you read that peramater correctly ;)
    {
        List<Vector3> vertexList = new List<Vector3>();
        List<int> triangleList = new List<int>();
        
        foreach (List<Vector3> lineSegment in allLineSegments)
        {
            Vector3 averageSegmentPosition = Vector3.zero;
            foreach (Vector3 point in lineSegment)
            {
                vertexList.Add(point);
                averageSegmentPosition += point;
            }

            averageSegmentPosition /= lineSegment.Count;
            vertexList.Add(averageSegmentPosition);
            int averageBlipIndex = vertexList.IndexOf(averageSegmentPosition);

            int firstIndexOfLine = -1;
            foreach (Vector3 point in lineSegment)
            {
                if (lineSegment.IndexOf(point) == 0) { firstIndexOfLine = vertexList.IndexOf(point); }
                
                if (vertexList.IndexOf(point) + 1 == averageBlipIndex)
                {
                    triangleList.Add(firstIndexOfLine);
                }
                else
                {
                    triangleList.Add(vertexList.IndexOf(point) + 1);
                }
                triangleList.Add(vertexList.IndexOf(point));
                triangleList.Add(averageBlipIndex);
            }
        }

		mesh.Clear();
        mesh.SetVertices(vertexList);
        mesh.SetTriangles(triangleList, 0);
		mesh.Optimize();
		mesh.RecalculateNormals();
    }
}
