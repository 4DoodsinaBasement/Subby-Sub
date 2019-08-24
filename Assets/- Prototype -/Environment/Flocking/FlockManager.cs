using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    [Header("Flock Settings")]
    public GameObject fishPrefab;
    [Range(3, 300)]
    public int numberOfFish = 20;
    public Vector3 spawnBounds = new Vector3Int(5,5,5);
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

    Transform flockTarget;
    float averageSpeed;


    public Transform GetFlockTarget() { return flockTarget; }
    

    void Start()
    {
        flockTarget = transform.parent.Find("Flock Target");
        
        fishArray = new GameObject[numberOfFish];

        for (int i = 0; i < numberOfFish; i++)
        {
            Vector3 spawnPosition = this.transform.position + new Vector3( 
                Random.Range(-spawnBounds.x, spawnBounds.x), 
                Random.Range(-spawnBounds.y, spawnBounds.y), 
                Random.Range(-spawnBounds.z, spawnBounds.z));
            
            fishArray[i] = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
            fishArray[i].GetComponent<FlockController_Fish>().manager = this;
            fishArray[i].name = "Fish " + i;
            fishArray[i].transform.SetParent(this.transform);
        }
    }

    void Update()
    {
        averageSpeed = 0;
        foreach (GameObject fish in fishArray) { averageSpeed += fish.GetComponent<FlockController_Fish>().speed; }
        averageSpeed /= fishArray.Length;

        Debugging();
    }


    void Debugging()
    {
        Debugger.Log("Average Speed", averageSpeed, "{0:0.000}");
    }
}
