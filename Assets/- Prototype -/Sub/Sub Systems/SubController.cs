using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubController : MonoBehaviour
{
	Rigidbody rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		AdjustSpeed();
		UpdateBuoyancy();
		AdjustRotation();

		ReadOut();

		Debugging();
	}

	#region Health
	[Header("Health")]
	public int maxHealth = 100;
	public int currentHealth = 100;
	public void ModifyHealth(int value)
	{
		currentHealth += value;
		if (currentHealth > maxHealth) { currentHealth = maxHealth; }

		if (currentHealth <= 0)
		{
			Debug.Log("YOU DIED!!!!!!!!!!!!!!!");
		}
	}
	#endregion

	#region Throttle
	[Header("Throttle")]
	[ReadOnly] public float throttle = 0;
	public float accThrottle = 3.0f;
	public float maxSpeed_horizontal = 10f;

	void AdjustSpeed()
	{
		rb.AddRelativeForce(0, 0, rb.mass * (accThrottle * throttle));
		if (rb.velocity.magnitude > maxSpeed_horizontal) { rb.velocity = rb.velocity.normalized * maxSpeed_horizontal; }
	}
	#endregion

	#region Buoyancy
	[Header("Buoancy")]
	[ReadOnly] public float buoyancy = 0;
	public float accBuoyancy = 3;
	public float maxSpeed_vertical = 3.0f;

	void UpdateBuoyancy()
	{
		rb.AddRelativeForce(0, rb.mass * (accBuoyancy * buoyancy), 0);
		if (rb.velocity.magnitude > maxSpeed_horizontal) { rb.velocity = rb.velocity.normalized * maxSpeed_horizontal; }
	}
	#endregion

	#region Steering
	[Header("Steering")]
	[ReadOnly] public float steering = 0;
	public float accSteering = 1;

	void AdjustRotation()
	{
		rb.angularVelocity = new Vector3(0, (accSteering / 1000) * steering, 0);
	}
	#endregion

	#region Readout
	[Header("Readout Variables")]
	public Text SubHealth;
	public Text SteeringHeading;
	public Text SteeringTurning;
	public Text ThrottleSpeed;
	public Text ThrottlePower;
	public Text BuoyancyDepth;
	public Text BuoyancyPower;

	void ReadOut()
	{
		SubHealth.text = string.Format("{0} / {1} HP", currentHealth, maxHealth);
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