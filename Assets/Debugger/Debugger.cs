using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Debugger
{
    static string defaultFormat = "{0:0.0}";
    
    public static List<DebugObject> elements = new List<DebugObject>();
    static List<string> namesTaken = new List<string>();

    // Debugger.Log("Heading", transform.eulerAngles.y);
    public static void Log (string label, object value) { TestLog(label, value, defaultFormat); }

    // Debugger.Log("Heading", transform.eulerAngles.y, "{0:0}°");
    public static void Log (string label, object value, string format) { TestLog(label, value, format); }


    public static void TestLog (string label, object value, string format)
    {
        if (elements.Count == 0)
        {
            elements.Add(new DebugObject(label, value, format));
            namesTaken.Add(label);
        }
        else if (namesTaken.Contains(label) == false)
        {
            elements.Add(new DebugObject(label, value, format));
            namesTaken.Add(label);
        }
        else
        {
            foreach (DebugObject debugObject in Debugger.elements)
            {
                if (debugObject.label == label) { debugObject.value = value; }
            }
        }
    }
}


public class DebugObject
{
    public string label;
    public object value;
    public string format;

    public DebugObject(string label, object value, string format)
    {
        this.label = label;
        this.value = value;
        this.format = format;
    }
}
