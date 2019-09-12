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
        
    }

    void Update()
    {
        DeleteBlips();
        CreateBlips(resolution);
    }


    void CreateBlips(int RaysToShoot)
    {
        float angle = 0;
        for (int i = 0; i < RaysToShoot; i++)
        {
            float x = Mathf.Sin (angle);
            float z = Mathf.Cos (angle);
            angle += 2 * Mathf.PI / RaysToShoot;
        
            Vector3 direction = new Vector3 (x, 0, z);
            Vector3 relativePosition;

            RaycastHit hit;
            if (Physics.Raycast(castOrigin.position, direction, out hit, range, LayerMask.GetMask("Terrain")))
            {
                relativePosition = hit.point - castOrigin.position;
    
                relativePosition *= 0.01f;
                GameObject newBlip = Instantiate(mapBlip, drawOrigin.position + relativePosition, Quaternion.identity);

                blips.Add(newBlip);
                newBlip.transform.parent = drawOrigin.transform;
            }
        }
    }

    void DeleteBlips()
    {
        foreach (GameObject blip in blips) { Destroy(blip); }
    }
}
