using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController_Fish : MonoBehaviour
{
    public FlockManager manager;
    public float speed;
    float multiplier = 1.0f;


    void Start()
    {
        
    }

    
    void Update()
    {
        ApplyFlockRules();
        CalculateSpeed();
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
                flockSpeed += fish.GetComponent<FlockController_Fish>().speed;
            }
        }

        if (neighborGroupSize > 0)
        {
            vCenter = vCenter / neighborGroupSize + (manager.GetFlockTarget().position - this.transform.position);
            flockSpeed /= neighborGroupSize;

            Vector3 direction = (vCenter + vAvoid) - transform.position;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), manager.rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            vCenter = manager.GetFlockTarget().position - this.transform.position;
        }
    }

    void CalculateSpeed()
    {
        float distanceToTarget = Vector3.Distance(this.transform.position, manager.GetFlockTarget().position);
        float unclampedSpeed = ((Mathf.Pow(distanceToTarget, 1.5f)) / 5.0f);

        if ((int)Time.time % 3 == 0) { multiplier = Random.Range(1f, 2f); }

        unclampedSpeed *= multiplier;
        speed = Mathf.Clamp(unclampedSpeed, manager.minSpeed, manager.maxSpeed);
    }
}
