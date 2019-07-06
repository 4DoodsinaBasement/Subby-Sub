using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFlockController : MonoBehaviour
{
    public FlockManager manager;
    float speed;


    void Start()
    {
        
        speed = Random.Range(manager.minSpeed, manager.maxSpeed);
    }

    
    void Update()
    {
        ApplyFlockRules();
        speed = Mathf.Clamp(Vector3.Distance(this.transform.position, manager.flockTarget.position), manager.minSpeed, manager.maxSpeed);
        transform.Translate(0,0, speed * Time.deltaTime);
    }


    void ApplyFlockRules()
    {
        Vector3 vCenter = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float flockSpeed = 0.01f;
        int neighborGroupSize = 0;

        float currentNeighborDistance = 0;

        foreach (GameObject fish in manager.fishArray)
        if (fish != this.gameObject)
        {
            currentNeighborDistance = Vector3.Distance(fish.transform.position, this.transform.position);
            if (currentNeighborDistance <= manager.neighborDistance)
            {
                vCenter += fish.transform.position; neighborGroupSize++;
                if (currentNeighborDistance < manager.avoidDistance) { vAvoid += (this.transform.position - fish.transform.position); }
                flockSpeed += fish.GetComponent<FishFlockController>().speed;
            }
        }

        if (neighborGroupSize > 0)
        {
            vCenter = vCenter / neighborGroupSize + (manager.flockTarget.position - this.transform.position);
            flockSpeed /= neighborGroupSize;

            Vector3 direction = (vCenter + vAvoid) - transform.position;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), manager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
