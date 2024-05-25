public class FireCooldownMinus5:CardAction
{
    private PlayerManager playerInstance;

    public  void Start()
    {
        playerInstance = PlayerManager.Instance;
    }
    
    public override void CardDo()
    {
        playerInstance.fireCooldownModifier.StatValue += 0.05f;
    }
}