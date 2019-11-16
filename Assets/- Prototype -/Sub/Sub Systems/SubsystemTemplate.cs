using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubsystemTemplate : MonoBehaviour
{
	[HideInInspector]
	public SubsystemManager manager;
	public bool overrideMove = true, overrideLook = false;
	void Start()
	{
		manager = transform.parent.GetComponent<SubsystemManager>();
		if (manager == null)
		{
			Debug.LogError("Couldn't find SubSystemManager");
		}
	}

	public abstract void LeftStickX(float value);
	public abstract void LeftStickY(float value);
	public abstract void RightStickX(float value);
	public abstract void RightStickY(float value);

	public abstract void LeftStickClick();
	public abstract void LeftStickClick_Up();
	public abstract void LeftStickClick_Down();
	public abstract void RightStickClick();
	public abstract void RightStickClick_Up();
	public abstract void RightStickClick_Down();

	public abstract void ButtonNorth();
	public abstract void ButtonNorth_Up();
	public abstract void ButtonNorth_Down();
	public abstract void ButtonSouth();
	public abstract void ButtonSouth_Up();
	public abstract void ButtonSouth_Down();
	public abstract void ButtonEast();
	public abstract void ButtonEast_Up();
	public abstract void ButtonEast_Down();
	public abstract void ButtonWest();
	public abstract void ButtonWest_Up();
	public abstract void ButtonWest_Down();

	public abstract void LeftTrigger();
	public abstract void LeftTrigger_Up();
	public abstract void LeftTrigger_Down();
	public abstract void RightTrigger();
	public abstract void RightTrigger_Up();
	public abstract void RightTrigger_Down();
	public abstract void LeftBumper();
	public abstract void LeftBumper_Up();
	public abstract void LeftBumper_Down();
	public abstract void RightBumper();
	public abstract void RightBumper_Up();
	public abstract void RightBumper_Down();

	public abstract void PadNorth();
	public abstract void PadNorth_Up();
	public abstract void PadNorth_Down();
	public abstract void PadSouth();
	public abstract void PadSouth_Up();
	public abstract void PadSouth_Down();
	public abstract void PadEast();
	public abstract void PadEast_Up();
	public abstract void PadEast_Down();
	public abstract void PadWest();
	public abstract void PadWest_Up();
	public abstract void PadWest_Down();

	public abstract void ButtonStart();
	public abstract void ButtonStart_Up();
	public abstract void ButtonStart_Down();
	public abstract void ButtonSelect();
	public abstract void ButtonSelect_Up();
	public abstract void ButtonSelect_Down();
}
