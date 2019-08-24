using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerSystemControl))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerUI : MonoBehaviour
{
    public Text currentStationValue;
    public Color32 activeColor, inactiveColor;

    void Update()
    {
        if (GetComponent<PlayerSystemControl>().currentSystem != null)
        {
            currentStationValue.text = GetComponent<PlayerSystemControl>().currentSystem.gameObject.name;
            currentStationValue.color = activeColor;
        }
        else
        {
            currentStationValue.text = "None";
            currentStationValue.color = inactiveColor;
        }
    }
}
