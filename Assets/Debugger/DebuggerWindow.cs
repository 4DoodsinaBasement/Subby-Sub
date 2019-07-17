using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggerWindow : MonoBehaviour
{
    public GameObject debugValuePrefab;
    public GameObject emptyListPrefab;

    GameObject emptyListMessage;

    public Color32 unpausedColor;
    public Color32 pausedColor;

    public bool isEmpty;
    public bool isDisplayed;
    public bool pauseDebugging = false;
    int lastObjectPosition = 0;


    void Start()
    {
        Debug.Log(" ----- DEBUGGER RUNNING: Press Left & Right Control to show window ----- ");

        isEmpty = (Debugger.elements.Count == 0);

        emptyListMessage = Instantiate(emptyListPrefab, new Vector3(0,0,0), Quaternion.identity, transform);
        emptyListMessage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
        emptyListMessage.name = "Empty List Object";
        emptyListMessage.SetActive(isEmpty);
    }
    
    void Update()
    {
        CheckControls();
        CheckDisplay();
        CheckElements();
    }


    void CheckControls()
    {
        if  ((Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightControl)) || (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.RightControl)))
        {
            isDisplayed = !isDisplayed;
        }

        if (isDisplayed && Input.GetKeyDown(KeyCode.P))
        {
            pauseDebugging = !pauseDebugging;
        }
    }

    void CheckDisplay()
    {
        GetComponent<Image>().enabled = isDisplayed;
        emptyListMessage.SetActive(isDisplayed && isEmpty);
        foreach (Transform child in transform) { child.gameObject.SetActive(isDisplayed); }
    }

    void CheckElements()
    {
        isEmpty = (Debugger.elements.Count == 0);
        emptyListMessage.SetActive(isDisplayed && isEmpty);

        if (!pauseDebugging)
        {
            foreach (DebugObject debugObject in Debugger.elements)
            {
                if (transform.Find(debugObject.label) != null)
                {
                    transform.Find(debugObject.label).Find("Value").GetComponent<Text>().text = string.Format(debugObject.format, debugObject.value);
                    transform.Find(debugObject.label).Find("Value").GetComponent<Text>().color = unpausedColor;
                }
                else
                {
                    GameObject newObject =  Instantiate(debugValuePrefab, new Vector3(0,0,0), Quaternion.identity, transform);
                    newObject.name = debugObject.label;
                    newObject.transform.Find("Label").GetComponent<Text>().text = debugObject.label;
                    newObject.transform.Find("Value").GetComponent<Text>().text = string.Format(debugObject.format, debugObject.value);
                    newObject.transform.Find("Value").GetComponent<Text>().color = unpausedColor;
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,lastObjectPosition);
                    newObject.SetActive(isDisplayed);

                    lastObjectPosition -= 40;
                }
            }
        }
        else
        {
            foreach (DebugObject debugObject in Debugger.elements)
            {
                if (transform.Find(debugObject.label) != null)
                {
                    transform.Find(debugObject.label).Find("Value").GetComponent<Text>().color = pausedColor;
                }
                else
                {
                    GameObject newObject =  Instantiate(debugValuePrefab, new Vector3(0,0,0), Quaternion.identity, transform);
                    newObject.name = debugObject.label;
                    newObject.transform.Find("Label").GetComponent<Text>().text = debugObject.label;
                    newObject.transform.Find("Value").GetComponent<Text>().color = pausedColor;
                    newObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,lastObjectPosition);
                    newObject.SetActive(isDisplayed);

                    lastObjectPosition -= 40;
                }
            }
        }
    }
}
