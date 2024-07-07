using UnityEngine;

// [CreateAssetMenu(menuName = "Objects/HeartContainer")]
[CreateAssetMenu]
public class HeartContainerConfig: LootConfig
{
   public int hearts; 
   public override void LootDo()
   {
      PlayerManager.Instance.maxHealth += hearts ;
   }
}