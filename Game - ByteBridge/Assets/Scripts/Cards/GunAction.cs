using System;
using UnityEngine;
using UnityEngine.UI;

public class GunAction:CardAction
{
    [SerializeField] private GunConfig GunConfig;
    private PlayerManager playerInstance;

    public void Start()
    {
        
        playerInstance = PlayerManager.Instance;
        //setting sprite
        
    }

    public override void OnEnable()
    {
        itemSprite = GunConfig.gunSprite;
        base.OnEnable();
    }


    public override void CardDo()
    {
        playerInstance.AddGuns(GunConfig);
    }
}