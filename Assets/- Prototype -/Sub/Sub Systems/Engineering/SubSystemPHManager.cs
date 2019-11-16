using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSystemPHManager : MonoBehaviour
{
	[Header("Total Sub Health")]
	[ReadOnly] public float currentSubHealth;
	[Header("Hull")]
	public float maxHullHealth;
	[ReadOnly] public float currentHullHealth;

	[Header("Generator")]
	public float generatorMaxHealth; public int generatorMaxPower;
	[Header("Steering")]
	public float steeringMaxHealth; public int steeringMaxPower;
	[Header("Buoyancy")]
	public float buoyancyMaxHealth; public int buoyancyMaxPower;
	[Header("Throttle")]
	public float throttleMaxHealth; public int throttleMaxPower;
	[Header("Lazor")]
	public float lazorMaxHealth; public int lazorMaxPower;
	[Header("Sonar")]
	public float sonarMaxHealth; public int sonarMaxPower;
	[Header("Lights")]
	public float lightsMaxHealth; public int lightsMaxPower;
	[Header("Engineering")]
	public float engineeringMaxHealth; public int engineeringMaxPower;

	[Space(10)]
	[Header("Classes")]
	[ReadOnly] public SubSystemPH generator;
	[ReadOnly] public SubSystemPH engineering;
	[ReadOnly] public SubSystemPH steering;
	[ReadOnly] public SubSystemPH throttle;
	[ReadOnly] public SubSystemPH buoyancy;
	[ReadOnly] public SubSystemPH sonar;
	[ReadOnly] public SubSystemPH lights;
	[ReadOnly] public SubSystemPH lazor1;
	[ReadOnly] public SubSystemPH lazor2;
	public List<SubSystemPH> subSystemPHs = new List<SubSystemPH>();


	// --- Testing --- //
	[ContextMenu("Allocate From Generator To Steering")]
	void AllocateFromGeneratorToSteering()
	{
		AllocatePower(steering);
	}
	[ContextMenu("Damage Steering By 10")]
	void DamageSteeringBy10()
	{
		steering.currentHealth -= 10;
	}
	// --- Testing --- //


	void Start()
	{
		generator = new SubSystemPH("generator", generatorMaxHealth, generatorMaxPower); subSystemPHs.Add(generator);
		engineering = new SubSystemPH("engineering", engineeringMaxHealth, engineeringMaxPower); subSystemPHs.Add(engineering);
		steering = new SubSystemPH("steering", steeringMaxHealth, steeringMaxPower); subSystemPHs.Add(steering);
		throttle = new SubSystemPH("throttle", throttleMaxHealth, throttleMaxPower); subSystemPHs.Add(throttle);
		buoyancy = new SubSystemPH("buoyancy", buoyancyMaxHealth, buoyancyMaxPower); subSystemPHs.Add(buoyancy);
		sonar = new SubSystemPH("sonar", sonarMaxHealth, sonarMaxPower); subSystemPHs.Add(sonar);
		lights = new SubSystemPH("lights", lightsMaxHealth, lightsMaxPower); subSystemPHs.Add(lights);
		lazor1 = new SubSystemPH("lazor1", lazorMaxHealth, lazorMaxPower); subSystemPHs.Add(lazor1);
		lazor2 = new SubSystemPH("lazor2", lazorMaxHealth, lazorMaxPower); subSystemPHs.Add(lazor2);

		currentHullHealth = maxHullHealth;
		InitializeGenerator();

		UpdateSubHealth();
	}

	void Update()
	{
		UpdateSubHealth();
	}

	void InitializeGenerator()
	{
		generator.currentPower = generator.maxPower;

		foreach (SubSystemPH system in subSystemPHs)
		{
			if (system != generator && generator.currentPower > 0)
			{
				system.currentPower++;
				generator.currentPower--;
			}
		}
	}

	void UpdateSubHealth()
	{
		float newSubHealth = 0;
		foreach (SubSystemPH item in subSystemPHs)
		{
			newSubHealth += item.currentHealth;
		}
		currentSubHealth = newSubHealth;
	}

	public void AllocatePower(SubSystemPH subsystem)
	{
		if (subsystem.currentPower < subsystem.currentMaxPower && generator.currentPower > 0)
		{
			subsystem.currentPower++;
			generator.currentPower--;
		}
	}

	public void DeallocatePower(SubSystemPH subsystem)
	{
		if (subsystem.currentPower > 0)
		{
			subsystem.currentPower--;
			generator.currentPower++;
		}
	}

	public void RepairSubSystem(SubSystemPH systemToRepair, float amount)
	{
		if (systemToRepair == null) { return; }
		else
		{
			systemToRepair.currentHealth += amount;
		}
	}
}

[System.Serializable]
public class SubSystemPH
{
	public string name;

	[ReadOnly] public float maxHealth;
	[HideInInspector] public float currentMaxHealth;
	[SerializeField] [ReadOnly] private float _currentHealth; public float currentHealth
	{
		get { return _currentHealth; }
		set
		{
			currentMaxPower = (int)Mathf.Ceil((float)(maxPower) * (value / maxHealth));
			_currentHealth = value;
			if (_currentHealth > maxHealth) { _currentHealth = maxHealth; }
			if (_currentHealth < 0) { _currentHealth = 0; }
		}
	}

	[ReadOnly] public int maxPower;
	[HideInInspector] public int currentMaxPower;
	[SerializeField] [ReadOnly] private int _currentPower; public int currentPower
	{
		get { return _currentPower; }
		set
		{
			_currentPower = value;
			if (_currentPower > maxPower) { _currentPower = maxPower; }
			if (_currentPower < 0) { _currentPower = 0; }
		}
	}

	public SubSystemPH(string name, float maxHealth, int maxPower)
	{
		this.name = name;

		this.maxHealth = maxHealth;
		this.currentMaxHealth = maxHealth;
		this.currentHealth = maxHealth;

		this.maxPower = maxPower;
		this.currentMaxPower = maxPower;
	}
}
