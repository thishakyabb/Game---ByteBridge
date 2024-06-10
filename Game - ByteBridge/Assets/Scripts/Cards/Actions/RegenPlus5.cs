public class RegenPlus5:CardAction
 {
     private PlayerManager playerInstance;
 
     public void Start()
     {
         playerInstance = PlayerManager.Instance;
     }
     
     public override void CardDo()
     {
         playerInstance.regenModifier.StatValue += 0.05f;
     }
 }