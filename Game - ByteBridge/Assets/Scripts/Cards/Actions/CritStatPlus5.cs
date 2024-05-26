public class CritStatPlus5:CardAction
{
    private PlayerManager playerInstance;

    public void Start()
    {
        playerInstance = PlayerManager.Instance;
    }
    
    public override void CardDo()
    {
        playerInstance.critStat.StatValue += 0.05f;
    }
}