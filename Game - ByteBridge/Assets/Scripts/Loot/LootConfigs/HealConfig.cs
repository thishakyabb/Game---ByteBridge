using UnityEngine;

[CreateAssetMenu]
public class HealConfig: LootConfig

{
   public override void LootDo()
   {
      PlayerManager.Instance.currentHealth = PlayerManager.Instance.maxHealth ;
   }
}