using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    public GameObject enemyPrefab;
   [SerializeField] private GameObject xPrefab;

   [SerializeField] private float warningDuration = 3f;
   private float currentWarningTime = 0f;
   private GameObject warning;
   private bool spawned = false;
   private LootBag lootBag;
   private bool lootSpawned = false;
   public Vector3 lastPosition;
   private void Start()
   {
      warning = Instantiate(xPrefab, gameObject.transform);
      lootBag = GetComponent<LootBag>();
      StartCoroutine(Count());
   }

   private void Update()
   {
      if (spawned)
      {
         var enemy = gameObject.GetComponentInChildren<EnemyBrain>();
         
         if (enemy == null && lootSpawned == false)
         {
            lootBag.SpawnLoot(lastPosition);
            lootSpawned = true;
            Destroy(gameObject);
         }
         return;
      }
      if (currentWarningTime >= warningDuration && spawned == false)
      {
         Destroy(warning);
         Instantiate(enemyPrefab, gameObject.transform);
         spawned = true;
      }
   }
    private IEnumerator Count()
   {
      while (currentWarningTime <= warningDuration)
      {
         yield return new WaitForSeconds(1);
         currentWarningTime++;
      }

   }
}
