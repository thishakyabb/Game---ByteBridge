public class MovementSpeedPlus5:CardAction
{
    private PlayerManager playerInstance;

    public  void Start()
    {
        playerInstance = PlayerManager.Instance;
    }
    
    public override void CardDo()
    {
        playerInstance.movementSpeedModifier.StatValue += 0.05f;
    }
}