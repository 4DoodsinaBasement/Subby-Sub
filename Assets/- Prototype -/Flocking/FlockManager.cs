using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    [Header("Flock Settings")]
    public GameObject fishPrefab;
    public int numberOfFish = 20;
    public Transform flockTarget;
    public Vector3 swimBounds = new Vector3Int(5,5,5);
    public GameObject[] fishArray;

    [Header("Fish Settings")]
    public float minSpeed = 1.0f;
    public float maxSpeed = 3.0f;
    [Range(1.0f, 10.0f)]
    public float neighborDistance = 2.0f;
    [Range(1.0f, 10.0f)]
    public float avoidDistance = 2.0f;
    [Range(1.0f, 5.0f)]
    public float rotationSpeed = 3.0f;

    float averageSpeed;
    

    void Start()
    {
        flockTarget = transform.parent.Find("Flock Target");
        
        fishArray = new GameObject[numberOfFish];

        for (int i = 0; i < numberOfFish; i++)
        {
            Vector3 spawnPosition = this.transform.position + new Vector3( 
                Random.Range(-swimBounds.x, swimBounds.x), 
                Random.Range(-swimBounds.y, swimBounds.y), 
                Random.Range(-swimBounds.z, swimBounds.z));
            
            fishArray[i] = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
            fishArray[i].GetComponent<FishFlockController>().manager = this;
            fishArray[i].name = "Fish " + i;
            fishArray[i].transform.SetParent(this.transform);
        }
    }

    void Update()
    {
        averageSpeed = 0;
        foreach (GameObject fish in fishArray) { averageSpeed += fish.GetComponent<FishFlockController>().speed; }
        averageSpeed /= fishArray.Length;

        Debugging();
    }


    void Debugging()
    {
        Debugger.Log("Average Speed", averageSpeed, "{0:0.000}");
    }
}
