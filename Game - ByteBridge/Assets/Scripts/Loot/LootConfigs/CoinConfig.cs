using UnityEngine;

[CreateAssetMenu]
public class CoinConfig: LootConfig  
{
   public int coins; 
   public override void LootDo()
   {
      PlayerManager.Instance.coins += coins ;
      PlayerManager.Instance.UpdateCoins();
   }
}