using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class missionScript : MonoBehaviour
{
    public GameObject MineCount; 
    public Text MissionValueText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MissionValueText.text = MineCount.transform.childCount.ToString(); 
    }
}
