using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class GunAction:CardAction
{
    [SerializeField] public GunConfig GunConfig;
    private PlayerManager playerInstance;
    [SerializeField] private TextMeshProUGUI firerateTMP;
    [SerializeField] private TextMeshProUGUI damageTMP;
    [SerializeField] private TextMeshProUGUI spreadTMP;
    [SerializeField] private TextMeshProUGUI piercingTMP;
    [SerializeField] private TextMeshProUGUI lifestealTMP;
    [SerializeField] private GameObject rarityHolder;
    [SerializeField] private GameObject circle;
    
    

    public void Start()
    {
        
        playerInstance = PlayerManager.Instance;
        //setting sprite
        
    }

    public override void OnEnable()
    {
        //Instantiate a totally unique gunconfig and save it to instance variable GunConfig
        var _gunconfig = Object.Instantiate(GunConfig);
        _gunconfig.GenerateFullConfig();
        GunConfig = _gunconfig;
        
        itemSprite = GunConfig.gunSprite;
        damageTMP.text = "Damage: " + GunConfig.damage.ToString("F0");
        firerateTMP.text = "Fire cooldown: " + GunConfig.fireRate.ToString("F2");
        spreadTMP.text = "Spread: \u00b1" + GunConfig.bulletSpread.ToString("F2")+"deg";
        piercingTMP.text = "Piercing: " + GunConfig.piercing.ToString("F0");
        lifestealTMP.text = "Life Steal: " + GunConfig.lifesteal.ToString("F2");
        for (int i = 0; i <= GunConfig.rarity; i++)
        {
            Instantiate(circle, rarityHolder.transform);
        }
        base.OnEnable();
    }


    public override void CardDo()
    {
        playerInstance.AddGuns(GunConfig);
    }
}