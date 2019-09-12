using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarTest : MonoBehaviour
{
    public GameObject worldBlip, mapBlip;
    public Transform castOrigin, drawOrigin;
    public int resolution = 100;
    public float range;

    List<GameObject> blips = new List<GameObject>();
    Vector3 lastHitPosition, thisHitPosition;
    
    
    void Start()
    {
        CreateBlips();

        UpdateBlips();
        UpdateLine();
    }

    void FixedUpdate()
    {
        if (Time.time % 1.0f == 0)
        {
        }
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
        /*
        Iterate through every blib and check if it's active
        if active, check if i+1 is active
        if both true, recur through next blip.
        store each active blip location
        when hit non-active, break recursion and make a line with array of positions
        */

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
            // set i = 1 to make sure everything is working properly
            for (int i = 0; i < remappedBlips.Count; i++)
            {
                int lastIndexChecked = GetNextPosition(i);
                if (i != lastIndexChecked) // If GetNextPosition() returns the same number as passed, it means the next blip is inactive
                {
                    // Debug.Log("There was supposed to be a wall here!");

                    List<GameObject> currentLineSegment = new List<GameObject>();
                    for (int currentPointInLineSegment = i; currentPointInLineSegment <= lastIndexChecked; currentPointInLineSegment++)
                    {
                        currentLineSegment.Add(remappedBlips[currentPointInLineSegment]);
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


        // TO-DO 
        /*
        After each recursion, make a line vertex at range for that level of recursion
         */
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

    // Yes, you read that peramater correctly ;)
    void CreateLineRenderer(List<List<GameObject>> allLineSegments)
    {
        foreach (List<GameObject> lineSegment in allLineSegments)
        {
            Debug.Log(" --- Starting Line Segment ---");
            foreach (GameObject point in lineSegment)
            {
                Debug.Log(point.name);
            }
        }
    }
}
