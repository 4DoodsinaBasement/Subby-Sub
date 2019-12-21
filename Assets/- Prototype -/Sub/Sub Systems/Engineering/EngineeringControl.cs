using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineeringControl : SubsystemTemplate
{
	void Update()
	{
		// engineering
		if (Input.GetKeyDown(KeyCode.A))
		{
			manager.subPHMmanager.AllocatePower(manager.subPHMmanager.engineering);
		}
		if (Input.GetKeyDown(KeyCode.Z))
		{
			manager.subPHMmanager.DeallocatePower(manager.subPHMmanager.engineering);
		}

		// steering
		if (Input.GetKeyDown(KeyCode.S))
		{
			manager.subPHMmanager.AllocatePower(manager.subPHMmanager.steering);
		}
		if (Input.GetKeyDown(KeyCode.X))
		{
			manager.subPHMmanager.DeallocatePower(manager.subPHMmanager.steering);
		}

		// throttle
		if (Input.GetKeyDown(KeyCode.D))
		{
			manager.subPHMmanager.AllocatePower(manager.subPHMmanager.throttle);
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			manager.subPHMmanager.DeallocatePower(manager.subPHMmanager.throttle);
		}

		// buoyancy
		if (Input.GetKeyDown(KeyCode.F))
		{
			manager.subPHMmanager.AllocatePower(manager.subPHMmanager.buoyancy);
		}
		if (Input.GetKeyDown(KeyCode.V))
		{
			manager.subPHMmanager.DeallocatePower(manager.subPHMmanager.buoyancy);
		}

		// sonar
		if (Input.GetKeyDown(KeyCode.G))
		{
			manager.subPHMmanager.AllocatePower(manager.subPHMmanager.sonar);
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			manager.subPHMmanager.DeallocatePower(manager.subPHMmanager.sonar);
		}

		// lights
		if (Input.GetKeyDown(KeyCode.H))
		{
			manager.subPHMmanager.AllocatePower(manager.subPHMmanager.lights);
		}
		if (Input.GetKeyDown(KeyCode.N))
		{
			manager.subPHMmanager.DeallocatePower(manager.subPHMmanager.lights);
		}

		// lazor1
		if (Input.GetKeyDown(KeyCode.J))
		{
			manager.subPHMmanager.AllocatePower(manager.subPHMmanager.lazor1);
		}
		if (Input.GetKeyDown(KeyCode.M))
		{
			manager.subPHMmanager.DeallocatePower(manager.subPHMmanager.lazor1);
		}

		// lazor2
		if (Input.GetKeyDown(KeyCode.K))
		{
			manager.subPHMmanager.AllocatePower(manager.subPHMmanager.lazor2);
		}
		if (Input.GetKeyDown(KeyCode.Comma))
		{
			manager.subPHMmanager.DeallocatePower(manager.subPHMmanager.lazor2);
		}
	}


	public override void LeftStickX(float value) { }
	public override void LeftStickY(float value) { }
	public override void RightStickX(float value) { }
	public override void RightStickY(float value) { }

	public override void LeftStickClick() { }
	public override void LeftStickClick_Up() { }
	public override void LeftStickClick_Down() { }
	public override void RightStickClick() { }
	public override void RightStickClick_Up() { }
	public override void RightStickClick_Down() { }

	public override void ButtonNorth() { }
	public override void ButtonNorth_Up() { }
	public override void ButtonNorth_Down() { }
	public override void ButtonSouth() { }
	public override void ButtonSouth_Up() { }
	public override void ButtonSouth_Down() { }
	public override void ButtonEast() { }
	public override void ButtonEast_Up() { }
	public override void ButtonEast_Down() { }
	public override void ButtonWest() { }
	public override void ButtonWest_Up() { }
	public override void ButtonWest_Down() { }

	public override void LeftTrigger() { }
	public override void LeftTrigger_Up() { }
	public override void LeftTrigger_Down() { }
	public override void RightTrigger() { }
	public override void RightTrigger_Up() { }
	public override void RightTrigger_Down() { }
	public override void LeftBumper() { }
	public override void LeftBumper_Up() { }
	public override void LeftBumper_Down() { }
	public override void RightBumper() { }
	public override void RightBumper_Up() { }
	public override void RightBumper_Down() { }

	public override void PadNorth() { }
	public override void PadNorth_Up() { }
	public override void PadNorth_Down() { }
	public override void PadSouth() { }
	public override void PadSouth_Up() { }
	public override void PadSouth_Down() { }
	public override void PadEast() { }
	public override void PadEast_Up() { }
	public override void PadEast_Down() { }
	public override void PadWest() { }
	public override void PadWest_Up() { }
	public override void PadWest_Down() { }

	public override void ButtonStart() { }
	public override void ButtonStart_Up() { }
	public override void ButtonStart_Down() { }
	public override void ButtonSelect() { }
	public override void ButtonSelect_Up() { }
	public override void ButtonSelect_Down() { }
}
