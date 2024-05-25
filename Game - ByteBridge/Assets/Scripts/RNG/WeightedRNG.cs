using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class WeightedRNG
{
    public float weight;
    public GameObject GameObject;

    public WeightedRNG(float weight, GameObject gameObject)
    {
        this.weight = weight;
        GameObject = gameObject;
    }
}

public class WeightedRNGCalulator
{
    public List<WeightedRNG> weightedList;
    

    public WeightedRNGCalulator(List<WeightedRNG> weightedObjs)
    {
        float count = 0f;
        foreach (var obj in weightedObjs)
        {
            count += obj.weight;
        }

        if (count != 1f)
        {
            throw new Exception("Weights don't add up to 1!");
        }

        weightedList = new List<WeightedRNG>();
        foreach (var obj in weightedObjs)
        {
            int numberOfEntries =  (int)Math.Round( obj.weight * 100f);
            for (int i = 0; i < numberOfEntries; i++)
            {
                weightedList.Add(obj);
            }
        }
    }

    public GameObject GetRandomGO()
    {
        return weightedList[Random.Range(0, 100)].GameObject;
    }
    
}