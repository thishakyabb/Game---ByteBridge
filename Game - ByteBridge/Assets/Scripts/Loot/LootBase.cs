using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  class LootBase:MonoBehaviour
{
   public PlayerManager PlayerManager;
   public LootConfig lootConfig; 
   [SerializeField]private float fadeTime = 8f;
   private float currentFadeTime = 0f;

   private void Start()
   {
      PlayerManager = PlayerManager.Instance;
      StartCoroutine(Count());
   }

   public void PostInstantiate()
   {
      GetComponentInChildren<SpriteRenderer>().sprite = lootConfig.lootSprite;
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

   private void OnCollisionEnter2D(Collision2D other)
   {
      var player = other.gameObject.GetComponent<PlayerMovement>();
      if (player != null)
      {
         if(lootConfig != null) lootConfig.LootDo();
         Destroy(gameObject);
      }
   }
}
