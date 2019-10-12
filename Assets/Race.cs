using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Race : MonoBehaviour
{

    public TextMeshPro display;
    public GameObject [] rings;
    
    int numberOfRings;
    
    void start ()
    {
        numberOfRings = 0; 
    }
    
    void update()
    {
        //if( )
    }

    void onTriggerEnter()
    {
        numberOfRings++;
    }

}
