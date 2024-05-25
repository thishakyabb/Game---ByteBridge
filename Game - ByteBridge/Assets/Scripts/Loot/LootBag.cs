using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField] public List<GameObject> lootList = new List<GameObject>();

    public void SpawnLoot(Vector3 position)
    {
        int randomPercentage = UnityEngine.Random.Range(0, 100);
        foreach (var loot in lootList)
        {
            if (loot.GetComponent<Loot>().dropChance > randomPercentage) 
            {
                // Instantiate(loot,position,Quaternion.identity, enemyTransform);
                Instantiate(loot, new Vector3(position.x,position.y,position.z), Quaternion.identity);
            }
        }
    }
}
