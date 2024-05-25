using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaxHealthPlusFiveAction:CardAction
{
    private PlayerManager playerInstance;

    public void Start()
    {
        playerInstance = PlayerManager.Instance;
    }
    
    
    public override void CardDo()
    {
        playerInstance.maxHealth += 5;
    }
}
