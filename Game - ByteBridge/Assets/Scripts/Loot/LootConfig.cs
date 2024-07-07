using UnityEngine;

public abstract class LootConfig: ScriptableObject
{ 
   public Sprite lootSprite;
   public AudioClip lootCollisionAudio;
   public string lootName;
   public float dropChance;
   public abstract void LootDo();
}