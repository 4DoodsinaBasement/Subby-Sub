using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggerWindow : MonoBehaviour
{
    public GameObject debugValuePrefab;

    public bool displayed;

    int lastObjectPosition = 0;


    void Start() { Debug.Log(" ----- DEBUGGER RUNNING: Press F1 to show window ----- "); }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            displayed = !displayed;
        }

        GetComponent<Image>().enabled = displayed;
        foreach (Transform child in transform) { child.gameObject.SetActive(displayed); }
        
        if (Debugger.elements.Count == 0)
        {
            
        }
        else
        {
            foreach (DebugObject debugObject in Debugger.elements)
            {
                if (transform.Find(debugObject.label) != null)
                {
                    transform.Find(debugObject.label).Find("Value").GetComponent<Text>().text = string.Format(debugObject.format, debugObject.value);
                }
                else
                {
                    GameObject newObject =  Instantiate(debugValuePrefab, new Vector3(0,0,0), Quaternion.identity, transform);
                    newObject.name = debugObject.label;
                    newObject.transform.Find("Label").GetComponent<Text>().text = debugObject.label;
                    newObject.transform.Find("Value").GetComponent<Text>().text = string.Format(debugObject.format, debugObject.value);
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,lastObjectPosition);
                    lastObjectPosition -= 40;
                }
            }
        }
    }
}
