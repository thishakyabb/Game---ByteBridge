public class DamagePlus5: CardAction
{
    private PlayerManager playerInstance;

    public void Start()
    {
        playerInstance = PlayerManager.Instance;
    }
    
    public override void CardDo()
    {
        playerInstance.damageModifier.StatValue += 0.05f;
    }
}