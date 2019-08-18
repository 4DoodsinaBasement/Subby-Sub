using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMaster))]
public class PlayerSystemControl : MonoBehaviour
{
    PlayerInfo player;
    public SubsystemTemplate observedSystem;
    public SubsystemTemplate currentSystem;


    void Awake()
    {
        player = GetComponent<PlayerMaster>().LoadPlayer();
    }

    void Update()
    {

        if (player.GetButtonDown("EnterSystem") && observedSystem != null)
        {
            EnterSystem();
        }
        else
        {
            SendInputToSubSystem();
        }
    }

    public void EnterSystem() { player.type = PlayerType.SubSystem; currentSystem = observedSystem; }
    public void ExitSystem() { player.type = PlayerType.Default; currentSystem = null; }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "System")
        {
            Debug.Log(other.name);
            if (other.gameObject.GetComponent<SubsystemTemplate>() != null) { observedSystem = other.GetComponent<SubsystemTemplate>(); }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "System")
        {
            if (other.gameObject.GetComponent<SubsystemTemplate>() != null) { observedSystem = null; }
        }
    }

    #region Inputs

    void SendInputToSubSystem()
    {
        if (currentSystem != null)
        {
            currentSystem.LeftStickX(player.GetAxis("LeftStickX"));
            currentSystem.LeftStickY(player.GetAxis("LeftStickY"));
            currentSystem.RightStickX(player.GetAxis("RightStickX"));
            currentSystem.RightStickY(player.GetAxis("RightStickY"));

            if (player.GetButton("LeftStickClick")) { currentSystem.LeftStickClick(); }
            if (player.GetButtonUp("LeftStickClick")) { currentSystem.LeftStickClick_Up(); }
            if (player.GetButtonDown("LeftStickClick")) { currentSystem.LeftStickClick_Down(); }
            if (player.GetButton("RightStickClick")) { currentSystem.RightStickClick(); }
            if (player.GetButtonUp("RightStickClick")) { currentSystem.RightStickClick_Up(); }
            if (player.GetButtonDown("RightStickClick")) { currentSystem.RightStickClick_Down(); }

            if (player.GetButton("ButtonNorth")) { currentSystem.ButtonNorth(); }
            if (player.GetButtonUp("ButtonNorth")) { currentSystem.ButtonNorth_Up(); }
            if (player.GetButtonDown("ButtonNorth")) { currentSystem.ButtonNorth_Down(); }
            if (player.GetButton("ButtonSouth")) { currentSystem.ButtonSouth(); }
            if (player.GetButtonUp("ButtonSouth")) { currentSystem.ButtonSouth_Up(); }
            if (player.GetButtonDown("ButtonSouth")) { currentSystem.ButtonSouth_Down(); }
            if (player.GetButton("ButtonEast")) { currentSystem.ButtonEast(); }
            if (player.GetButtonUp("ButtonEast")) { currentSystem.ButtonEast_Up(); }
            if (player.GetButtonDown("ButtonEast")) { currentSystem.ButtonEast_Down(); }
            if (player.GetButton("ButtonWest")) { currentSystem.ButtonWest(); }
            if (player.GetButtonUp("ButtonWest")) { currentSystem.ButtonWest_Up(); }
            if (player.GetButtonDown("ButtonWest"))
            {
                currentSystem.ButtonWest_Down();
                ExitSystem();
            }

            if (player.GetButton("LeftTrigger")) { currentSystem.LeftTrigger(); }
            if (player.GetButtonUp("LeftTrigger")) { currentSystem.LeftTrigger_Up(); }
            if (player.GetButtonDown("LeftTrigger")) { currentSystem.LeftTrigger_Down(); }
            if (player.GetButton("RightTrigger")) { currentSystem.RightTrigger(); }
            if (player.GetButtonUp("RightTrigger")) { currentSystem.RightTrigger_Up(); }
            if (player.GetButtonDown("RightTrigger")) { currentSystem.RightTrigger_Down(); }
            if (player.GetButton("LeftBumper")) { currentSystem.LeftBumper(); }
            if (player.GetButtonUp("LeftBumper")) { currentSystem.LeftBumper_Up(); }
            if (player.GetButtonDown("LeftBumper")) { currentSystem.LeftBumper_Down(); }
            if (player.GetButton("RightBumper")) { currentSystem.RightBumper(); }
            if (player.GetButtonUp("RightBumper")) { currentSystem.RightBumper_Up(); }
            if (player.GetButtonDown("RightBumper")) { currentSystem.RightBumper_Down(); }

            if (player.GetButton("PadNorth")) { currentSystem.PadNorth(); }
            if (player.GetButtonUp("PadNorth")) { currentSystem.PadNorth_Up(); }
            if (player.GetButtonDown("PadNorth")) { currentSystem.PadNorth_Down(); }
            if (player.GetButton("PadSouth")) { currentSystem.PadSouth(); }
            if (player.GetButtonUp("PadSouth")) { currentSystem.PadSouth_Up(); }
            if (player.GetButtonDown("PadSouth")) { currentSystem.PadSouth_Down(); }
            if (player.GetButton("PadEast")) { currentSystem.PadEast(); }
            if (player.GetButtonUp("PadEast")) { currentSystem.PadEast_Up(); }
            if (player.GetButtonDown("PadEast")) { currentSystem.PadEast_Down(); }
            if (player.GetButton("PadWest")) { currentSystem.PadWest(); }
            if (player.GetButtonUp("PadWest")) { currentSystem.PadWest_Up(); }
            if (player.GetButtonDown("PadWest")) { currentSystem.PadWest_Down(); }

            if (player.GetButton("ButtonStart")) { currentSystem.ButtonStart(); }
            if (player.GetButtonUp("ButtonStart")) { currentSystem.ButtonStart_Up(); }
            if (player.GetButtonDown("ButtonStart")) { currentSystem.ButtonStart_Down(); }
            if (player.GetButton("ButtonSelect")) { currentSystem.ButtonSelect(); }
            if (player.GetButtonUp("ButtonSelect")) { currentSystem.ButtonSelect_Up(); }
            if (player.GetButtonDown("ButtonSelect")) { currentSystem.ButtonSelect_Down(); }
        }
    }
    #endregion
}
