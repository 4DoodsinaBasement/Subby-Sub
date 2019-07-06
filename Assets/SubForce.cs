using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubForce : MonoBehaviour
{
    Rigidbody rb;
    
    [Header("Throttle")]
    float throttle = 0;
    public float deltaThrottle = 30; // (percent / second)
    public float accThrottle = 3.0f;
    public float maxSpeed_horizontal = 10f;

    [Header("Buoancy")]
    float buoyancy = 0;
    public float deltaBuoyancy = 30; // (percent / second)
    public float accBuoyancy = 3;
    public float maxSpeed_vertical = 3.0f;

    [Header("Yaw")]
    float yaw = 0;
    public float deltaYaw = 25f; // (degrees / second)
    public float accYaw = 1;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Throttle();
        Buoyancy();
        Yaw();

        Debugging();
    }

    void Throttle()
    {
        if (Input.GetKey(KeyCode.W)) { throttle += (deltaThrottle / 100 * Time.deltaTime); }
        if (Input.GetKey(KeyCode.S)) { throttle -= (deltaThrottle / 100 * Time.deltaTime); }
        if (Input.GetKeyDown(KeyCode.X)) { throttle = 0; }
        throttle = Mathf.Clamp(throttle, -0.5f, 1.0f);

        rb.AddRelativeForce(0,0,rb.mass * (accThrottle * throttle));

        if(rb.velocity.magnitude > maxSpeed_horizontal) { rb.velocity = rb.velocity.normalized * maxSpeed_horizontal; }
    }

    void Buoyancy()
    {
        if (Input.GetKey(KeyCode.UpArrow)) { buoyancy += (deltaBuoyancy / 100 * Time.deltaTime); }
        if (Input.GetKey(KeyCode.DownArrow)) { buoyancy -= (deltaBuoyancy / 100 * Time.deltaTime); }
        if (Input.GetKeyDown(KeyCode.X)) { buoyancy = 0; }
        buoyancy = Mathf.Clamp(buoyancy, -1.0f, 1.0f);

        rb.AddRelativeForce(0, rb.mass * (accBuoyancy * buoyancy), 0);

        if(rb.velocity.magnitude > maxSpeed_horizontal) { rb.velocity = rb.velocity.normalized * maxSpeed_horizontal; }
    }

    void Yaw()
    {
        if (Input.GetKey(KeyCode.D)) { yaw += (deltaYaw * Time.deltaTime); }
        if (Input.GetKey(KeyCode.A)) { yaw -= (deltaYaw * Time.deltaTime); }
        if (Input.GetKeyDown(KeyCode.X)) { yaw = 0; }
        yaw = Mathf.Clamp(yaw, -45.0f, 45.0f);

        rb.AddTorque(transform.up * (accYaw * rb.mass) * yaw);
    }


    void Debugging()
    {
        Debugger.Log("Speed", rb.velocity.magnitude, "{0:0.00}");
        Debugger.Log("Throttle", throttle * 100, "{0:0}%");
        Debugger.Log("Buoyancy", buoyancy * 100, "{0:0}%");
        Debugger.Log("Yaw", yaw, "{0:0}°");
        Debugger.Log("Heading", transform.eulerAngles.y, "{0:0}°");
    }
}
