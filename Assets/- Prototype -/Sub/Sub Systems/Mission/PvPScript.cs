using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PvPScript : MonoBehaviour
{   
    public GameObject EnemySub;
    public Text MissionObjective;
    public Text MissionValueText; 


    // Start is called before the first frame update
    void Start()
    {
        MissionObjective.text = "Defeat Enemy Sub";
    }

    // Update is called once per frame
    void Update()
    {
        MissionValueText.text = "Enemy Health: " + EnemySub.GetComponent<HPManager>().currentHP.ToString("F2");
    }
}
