using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SubSystemPHManager : MonoBehaviour
{
	[Header("Total Sub Health")]
	[ReadOnly] public float currentSubHealth;
	[Header("Hull")]
	public float maxHullHealth;
	[ReadOnly] public float currentHullHealth;
	[Header("Damage Group Size")]
	public float damageGroupSize;

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

	[Space(20)]
	[Header("Classes")]
	[HideInInspector] public SubSystemPH generator;
	[HideInInspector] public SubSystemPH engineering;
	[HideInInspector] public SubSystemPH steering;
	[HideInInspector] public SubSystemPH throttle;
	[HideInInspector] public SubSystemPH buoyancy;
	[HideInInspector] public SubSystemPH sonar;
	[HideInInspector] public SubSystemPH lights;
	[HideInInspector] public SubSystemPH lazor1;
	[HideInInspector] public SubSystemPH lazor2;
	public List<SubSystemPH> subSystemPHs = new List<SubSystemPH>();


	// --- Testing --- //
	[ContextMenu("Allocate From Generator To Steering")]
	void DamageGeneratorBy10()
	{
		DamageSubsystem(generator, 10);
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

		UpdateSubTotalHealth();
	}

	void Update()
	{
		UpdateSubTotalHealth();
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

	void UpdateSubTotalHealth()
	{
		float newSubHealth = 0;
		foreach (SubSystemPH item in subSystemPHs)
		{
			newSubHealth += item.currentHealth;
		}
		currentSubHealth = newSubHealth;
	}

	public void AssignDamage(float damage)
	{
		float damageRemaining = damage;

		List<SubSystemPH> activeSubsystems = subSystemPHs.Where(i => i.currentHealth > 0).ToList();

		while (damageRemaining > 0)
		{
			SubSystemPH subSystemToDamage = activeSubsystems[(int)Random.Range(0, subSystemPHs.Count)];
			if (subSystemToDamage != null)
			{
				float damageToDeal = (damageRemaining >= damageGroupSize) ? damageGroupSize : damageRemaining;

				if (subSystemToDamage.currentHealth >= damageToDeal)
				{
					subSystemToDamage.currentHealth -= damageToDeal;
				}
				else
				{
					damageToDeal -= subSystemToDamage.currentHealth;
					subSystemToDamage.currentHealth = 0;
					activeSubsystems.Remove(subSystemToDamage);
				}

				UpdateSystemPower(subSystemToDamage);
				damageRemaining -= damageToDeal;
			}
			else
			{
				Debug.Log("There was " + damageRemaining + " damage remaining and YOU DIED!");
				break;
			}
		}
	}

	public void DamageSubsystem(SubSystemPH subSystemToDamage, float damage)
	{
		if (subSystemToDamage.currentHealth >= damage)
		{
			subSystemToDamage.currentHealth -= damage;
		}
		else
		{
			subSystemToDamage.currentHealth = 0;
		}

		// UpdateSystemPower(subSystemToDamage);
	}

	void UpdateSystemPower(SubSystemPH systemToUpdate)
	{
		if (systemToUpdate.name.ToLower() != "generator")
		{
			while (systemToUpdate.currentPower != 0 && systemToUpdate.currentPower > systemToUpdate.currentMaxPower)
			{
				DeallocatePower(systemToUpdate);
			}
		}
		else // Update the Generator
		{
			int totalPowerAllocated = 0;
			foreach (SubSystemPH item in subSystemPHs) { totalPowerAllocated += item.currentPower; }
			while (systemToUpdate.currentPower + totalPowerAllocated > systemToUpdate.currentMaxPower)
			{
				Debug.Log("Please don't be infinite");
				if (systemToUpdate.currentPower > 0)
				{
					systemToUpdate.currentPower--;
				}
				else
				{
					DeallocatePower(subSystemPHs[(int)Random.Range(0, subSystemPHs.Count - 1)]);
					systemToUpdate.currentPower--;
				}
			}
		}
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
	[ReadOnly] public float currentMaxHealth;
	[SerializeField] [ReadOnly] private float _currentHealth; public float currentHealth
	{
		get { return _currentHealth; }
		set
		{
			_currentHealth = value;
			if (_currentHealth > maxHealth) { _currentHealth = maxHealth; }
			if (_currentHealth < 0) { _currentHealth = 0; }
			currentMaxPower = (int)Mathf.Ceil((float)(maxPower) * (_currentHealth / maxHealth));
		}
	}

	[ReadOnly] public int maxPower;
	[ReadOnly] public int currentMaxPower;
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
