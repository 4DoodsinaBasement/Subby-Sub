using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSystemPHManager : MonoBehaviour
{
	[Header("Total Sub Health")]
    [ReadOnly] public float currentSubHealth;
    [Header("Hull")]
    public float maxHullHealth;
    [ReadOnly]public float currentHullHealth;

	[Header("Generator")] 
    public float generatorMaxHealth; public int generatorMaxPower;
	[Header("Steering")] 
    public float steeringMaxHealth; public int steeringMaxPower;
	[Header("Buoyancy")] 
    public float buoyancyMaxHealth; public int buoyancyMaxPower;
	[Header("Throttle")] 
    public float throttleMaxHealth; public int throttleMaxPower;
	[Header("Laser")] 
    public float laserMaxHealth; public int laserMaxPower;
	[Header("Sonar")] 
    public float sonarMaxHealth; public int sonarMaxPower;
	[Header("Lights")] 
    public float lightsMaxHealth; public int lightsMaxPower;
	[Header("Engineering")] 
    public float engineeringMaxHealth; public int engineeringMaxPower;

    [Space(10)]
	[Header("Classes")]
    [ContextMenuItem("Allocate From Generator To Steering", "AllocateFromGeneratorToSteering")]
	[ReadOnly] public SubSystemPH generator;
	[ReadOnly] public SubSystemPH steering;
	[ReadOnly] public SubSystemPH buoyancy;
	[ReadOnly] public SubSystemPH throttle;
	[ReadOnly] public SubSystemPH laser1;
	[ReadOnly] public SubSystemPH laser2;
	[ReadOnly] public SubSystemPH sonar;
	[ReadOnly] public SubSystemPH lights;
	[ReadOnly] public SubSystemPH engineering;

	List<SubSystemPH> subSystemPHs = new List<SubSystemPH>();


    // --- Testing
    void AllocateFromGeneratorToSteering()
    {
        AllocatePower(steering);
    }
    // --- Testing
	
    
    void Start()
	{
		generator = new SubSystemPH(generatorMaxHealth, generatorMaxPower);

		steering = new SubSystemPH(steeringMaxHealth, steeringMaxPower);
		buoyancy = new SubSystemPH(buoyancyMaxHealth, buoyancyMaxPower);
		throttle = new SubSystemPH(throttleMaxHealth, throttleMaxPower);
		laser1 = new SubSystemPH(laserMaxHealth, laserMaxPower);
		laser2 = new SubSystemPH(laserMaxHealth, laserMaxPower);
		sonar = new SubSystemPH(sonarMaxHealth, sonarMaxPower);
		lights = new SubSystemPH(lightsMaxHealth, lightsMaxPower);
		engineering = new SubSystemPH(engineeringMaxHealth, engineeringMaxPower);
        
        subSystemPHs.Add(generator);

		subSystemPHs.Add(steering);
		subSystemPHs.Add(buoyancy);
		subSystemPHs.Add(throttle);
		subSystemPHs.Add(laser1);
		subSystemPHs.Add(laser2);
		subSystemPHs.Add(sonar);
		subSystemPHs.Add(lights);
		subSystemPHs.Add(engineering);

        generator.currentPower = 5;

		currentHullHealth = maxHullHealth;
		UpdateSubHealth();
	}

	void Update()
	{
		UpdateSubHealth();
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
		generator.currentPower--;
		subsystem.currentPower++;
	}

	public void DeallocatePower(SubSystemPH subsystem)
	{
		generator.currentPower++;
		subsystem.currentPower--;
	}
}

[System.Serializable]
public class SubSystemPH
{
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

	public SubSystemPH(float maxHealth, int maxPower)
	{
		this.maxHealth = maxHealth;
		this.currentMaxHealth = maxHealth;
		this.currentHealth = maxHealth;

		this.maxPower = maxPower;
		this.currentMaxPower = maxPower;
		this._currentPower = 1;
	}
}
