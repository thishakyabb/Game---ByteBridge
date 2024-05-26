using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Loot:MonoBehaviour
{
   public string lootName;
   public int dropChance;
   public PlayerManager PlayerManager;
   [SerializeField]private float fadeTime = 8f;
   private float currentFadeTime = 0f;
   public Loot(string lootName, int dropChance)
   {
      this.lootName = lootName;
      this.dropChance = dropChance;
   }

   private void Start()
   {
      PlayerManager = PlayerManager.Instance;
      StartCoroutine(Count());
   }

   private void Update()
   {
      if (currentFadeTime >= fadeTime)
      {
         Destroy(gameObject);
      }
   }

   private IEnumerator Count()
   {
      
      while (currentFadeTime < fadeTime)
      {
         yield return new WaitForSeconds(1);
         currentFadeTime++;
      }

   }

   public void LootDo()
   {
      PlayerManager.coins += 1;
      PlayerManager.UpdateCoins();
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      var player = other.gameObject.GetComponent<PlayerMovement>();
      if (player != null)
      {
         LootDo();
         Destroy(gameObject);
      }
   }
}
