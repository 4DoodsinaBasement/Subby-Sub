using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubForce : MonoBehaviour
{
    Rigidbody rb;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        UpdateThrottle();
        UpdateBuoyancy();
        UpdateSteering();

        ReadOut();

        Debugging();
    }


    #region Throttle
    [Header("Throttle")]
    float throttle = 0;
    public float deltaThrottle = 30; // (percent / second)
    public float accThrottle = 3.0f;
    public float maxSpeed_horizontal = 10f;

    public void Throttle(float axis)
    {
        if (axis > 0)
        {
            throttle += (deltaThrottle / 100 * Time.deltaTime); 
            throttle = Mathf.Clamp(throttle, -0.5f, 1.0f);
        }
        else if (axis < 0)
        {
            throttle -= (deltaThrottle / 100 * Time.deltaTime);
            throttle = Mathf.Clamp(throttle, -0.5f, 1.0f);
        }
    }

    public void ThrottleZero() { throttle = 0; }

    void UpdateThrottle()
    {
        rb.AddRelativeForce(0,0,rb.mass * (accThrottle * throttle));
        if(rb.velocity.magnitude > maxSpeed_horizontal) { rb.velocity = rb.velocity.normalized * maxSpeed_horizontal; }
    }
    #endregion

    #region Buoyancy
    [Header("Buoancy")]
    float buoyancy = 0;
    public float deltaBuoyancy = 30; // (percent / second)
    public float accBuoyancy = 3;
    public float maxSpeed_vertical = 3.0f;

    public void Buoyancy(float axis)
    {
        if (axis > 0)
        {
        buoyancy += (deltaBuoyancy / 100 * Time.deltaTime);
        buoyancy = Mathf.Clamp(buoyancy, -1.0f, 1.0f);
        }
        else if (axis < 0)
        {
        buoyancy -= (deltaBuoyancy / 100 * Time.deltaTime);
        buoyancy = Mathf.Clamp(buoyancy, -1.0f, 1.0f);
        }
    }

    public void BuoyancyZero() { buoyancy = 0; }

    void UpdateBuoyancy()
    {
        rb.AddRelativeForce(0, rb.mass * (accBuoyancy * buoyancy), 0);
        if(rb.velocity.magnitude > maxSpeed_horizontal) { rb.velocity = rb.velocity.normalized * maxSpeed_horizontal; }
    }
    #endregion

    #region Steering
    [Header("Steering")]
    float steering = 0;
    public float deltaSteering = 25f; // (degrees / second)
    public float accSteering = 1;

    public void Steering(float axis)
    {
        if (axis > 0)
        {
        steering += (deltaSteering * Time.deltaTime);
        steering = Mathf.Clamp(steering, -45.0f, 45.0f);
        }
        else if (axis < 0)
        {
        steering -= (deltaSteering * Time.deltaTime);
            steering = Mathf.Clamp(steering, -45.0f, 45.0f);
        }
    }

    public void SteeringZero() { steering = 0; }

    void UpdateSteering()
    {
        // rb.AddTorque(transform.up * (accSteering * rb.mass) * steering);
        rb.angularVelocity = new Vector3(0, (accSteering / rb.mass) * steering, 0);

    }
    #endregion

    #region Readout
    [Header("Readout Variables")]
    public Text SteeringHeading; 
    public Text SteeringTurning; 
    public Text ThrottleSpeed; 
    public Text ThrottlePower; 
    public Text BuoyancyDepth; 
    public Text BuoyancyPower;

    void ReadOut()
    {
        SteeringHeading.text = string.Format("{0:0}°", transform.eulerAngles.y);
        SteeringTurning.text = string.Format("{0:0}°", steering);
        ThrottleSpeed.text = string.Format("{0:0.00} m/s", rb.velocity.magnitude);
        ThrottlePower.text = string.Format("{0:0}%", throttle * 100);
        BuoyancyDepth.text = string.Format("{0:0} m", transform.position.y);
        BuoyancyPower.text = string.Format("{0:0}%", buoyancy * 100);
    }
    #endregion
    
    void Debugging()
    {
        Debugger.Log("Speed", rb.velocity.magnitude, "{0:0.00}");
        Debugger.Log("Throttle", throttle * 100, "{0:0}%");
        Debugger.Log("Buoyancy", buoyancy * 100, "{0:0}%");
        Debugger.Log("Steering", steering, "{0:0}°");
        Debugger.Log("Heading", transform.eulerAngles.y, "{0:0}°");
    }
}
