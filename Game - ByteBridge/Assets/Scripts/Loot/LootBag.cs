using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    [SerializeField] public List<LootConfig> lootList = new List<LootConfig>();
    private WeightedRNGCalulator _weightedRngCalulator;
    [SerializeField] public LootBase lootObject;

    private void Start()
    {
        List<WeightedRNGItem> weightedEnemyList = new List<WeightedRNGItem>();
        foreach (var config in lootList)
        {
            float weight = config.dropChance;
            weightedEnemyList.Add(new WeightedRNGItem(weight,config));
        }

        _weightedRngCalulator = new WeightedRNGCalulator(weightedEnemyList);
    }

    public void SpawnLoot(Vector3 position)
    {
      // Instantiate(loot,position,Quaternion.identity, enemyTransform);
      var randomconfig = _weightedRngCalulator.GetRandomGO();
      var l = Instantiate(lootObject, new Vector3(position.x,position.y,position.z), Quaternion.identity);
      l.lootConfig = randomconfig;
      l.PostInstantiate();
    }
}
