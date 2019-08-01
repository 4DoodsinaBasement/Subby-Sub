using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMaster))]
public class PlayerSystemControl : MonoBehaviour
{
    PlayerInfo player;

    public SubForce systems;
    public GameObject observedSystem;
    

    void Awake()
    {
        player = GetComponent<PlayerMaster>().LoadPlayer();
    }

    void Update()
    {
        if (player.GetButtonDown("EnterSystem") && observedSystem != null)
        {
            player.type = observedSystem.GetComponent<SubSystem>().stationType;
            Debug.Log(player.ID.ToString() + " entering " + player.type);
        }
        else if (player.GetButtonDown("ExitSystem")) { player.type = PlayerType.Default; }
        else
        {
            UpdateShipControls();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "System")
        {
            if (other.gameObject.GetComponent<SubSystem>() != null) { observedSystem = other.gameObject; }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "System")
        {
            if (other.gameObject.GetComponent<SubSystem>() != null) { observedSystem = null; }
        }
    }

    #region System Controls

    void UpdateShipControls()
    {
        systems.Throttle(player.GetAxis("Throttle"));
        systems.Buoyancy(player.GetAxis("Buoyancy"));
        systems.Steering(player.GetAxis("Steering"));

        if (player.GetButtonDown("Kill"))
        {
            switch (player.type)
            {
                case PlayerType.Throttle:
                    systems.ThrottleZero();
                    break;
                case PlayerType.Buoyancy:
                    systems.BuoyancyZero();
                    break;
                case PlayerType.Steering:
                    systems.SteeringZero();
                    break;
            }
        }
    }
    #endregion
}
