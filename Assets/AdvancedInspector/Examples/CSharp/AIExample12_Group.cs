﻿using UnityEngine;
using System.Collections;

using AdvancedInspector;

[AdvancedInspector]
public class AIExample12_Group : MonoBehaviour 
{
    // Item can be grouped using the Group attribute;
    [Inspect, Group("My First Group", 0)]
    public float myFirstField;

    [Inspect, Group("My First Group")]
    public float MyFirstProperty
    {
        get { return myFirstField; }
        set { myFirstField = value; }
    }

    [Inspect, Group("My First Group")]
    public void MyFirstMethod()
    {
        myFirstField++;
    }

    // Grouping is done with similar names.
    // The second parameter is the order in which the different groups are shown.
    [Inspect, Group("My Second Group", 1)]
    public float mySecondField;

    [Inspect, Group("My Second Group")]
    public float MySecondProperty
    {
        get { return mySecondField; }
        set { mySecondField = value; }
    }

    [Inspect, Group("My Second Group")]
    public void MySecondMethod()
    {
        mySecondField++;
    }
}
