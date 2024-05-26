using System;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public float StatValue;
       public String StatName;

    public Stat(float value, String name)
    {
        this.StatName = name;
        this.StatValue = value;
    }
}
