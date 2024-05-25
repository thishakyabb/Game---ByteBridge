public class RangePlusFiveAction:CardAction
{
    private PlayerManager playerInstance;

    public  void Start()
    {
        playerInstance = PlayerManager.Instance;
    }
    
    public override void CardDo()
    {
        playerInstance.rangeModifier.StatValue += 0.05f;
    }
}