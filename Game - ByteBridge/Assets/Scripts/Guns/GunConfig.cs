using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]
public class GunConfig : ScriptableObject
{

    [Header("General")]
    [SerializeField] public Sprite gunSprite;
    [SerializeField] public GameObject projectile;
    [SerializeField] public AudioClip fireAudioClip;

    [HideInInspector] public float fireRange = 10f;
    [HideInInspector] public float fireRate = 0.5f;
    [HideInInspector] public int piercing = 0;
    [HideInInspector] public float lifesteal = 0f;
    [HideInInspector] public int damage = 25;
    [HideInInspector] public int bulletCount = 1;
    [HideInInspector] public int bulletSpread = 0;
    [HideInInspector] public float bulletSpeed = 20f;
    [HideInInspector] public int rarity;
    
    [Header("Random Ranges")]
    public float fireRangeUpper = 10f;
    public float fireRangeLower = 10f;
    public float fireRateUpper = 0.5f;
    public float fireRateLower = 0.5f;

    public float lifestealUpper = 0f;
    public float lifestealLower = 0f;

    public int damageUpper = 25;
    public int damageLower = 25;
    [Range(0,35)] [SerializeField] public int bulletSpreadUpper = 0;
    [Range(0,35)] [SerializeField] public int bulletSpreadLower = 0;
    [SerializeField] public float bulletSpeedUpper = 20f;
    [SerializeField] public float bulletSpeedLower = 20f;

    private WeightedRNGCalulator _weightedRngCalulator;
    
    public float GenerateConfig(float rarity,float upper, float lower,bool lowerIsBetter = false)
    {
        float ceil, floor,first_quartile, second_quartile, third_quartile;
        if (rarity is >= 4 or < 0)
        {
            throw new Exception("Rarity must be between 0 and 4 ( 0 inclusive, 4 exclusive ) ");
        }

        second_quartile = (upper + lower) / 2;
        first_quartile = (second_quartile+ lower) / 2;
        third_quartile = (second_quartile + upper) / 2;

        if (!lowerIsBetter)
        {
            switch (rarity)
            {
               case 0:
                   ceil = first_quartile ;
                   floor = lower;
                   break;
               case 1:
                   ceil = second_quartile;
                   floor = first_quartile;
                   break;
               case 2:
                   ceil = third_quartile;
                   floor = second_quartile;
                   break;
               case 3:
                   ceil = upper;
                   floor = third_quartile;
                   break;
               default:
                   ceil = upper;
                   floor = lower;
                   break;
            }
            return MathF.Round(Random.Range(floor, ceil),2);
        }
        switch (rarity)
        {
            case 3:
                ceil = first_quartile ;
                floor = lower;
                break;
            case 2:
                ceil = second_quartile;
                floor = first_quartile;
                break;
            case 1:
                ceil = third_quartile;
                floor = second_quartile;
                break;
            case 0:
                ceil = upper;
                floor = third_quartile;
                break;
            default:
                ceil = upper;
                floor = lower;
                break;
        }
        return MathF.Round(Random.Range(floor, ceil),2);

    }

    public void GenerateFullConfig()
    {
        var rarity1 = new WeightedRNGItem(0.6f, 0);
        var rarity2 = new WeightedRNGItem(0.25f, 1);
        var rarity3 = new WeightedRNGItem(0.1f, 2);
        var rarity4 = new WeightedRNGItem(0.05f, 3);
        List<WeightedRNGItem> rngItems = new List<WeightedRNGItem>{rarity1, rarity2, rarity3, rarity4};
        _weightedRngCalulator = new WeightedRNGCalulator(rngItems) ;
        int finalRarity = _weightedRngCalulator.GetRandomGO();
        rarity = finalRarity;
        fireRate = GenerateConfig(finalRarity, fireRateUpper, fireRateLower);
        fireRange = GenerateConfig(finalRarity, fireRangeUpper, fireRangeLower);
        damage = (int)GenerateConfig(finalRarity, damageUpper, damageLower);
        lifesteal = GenerateConfig(finalRarity, lifestealUpper, lifestealLower);
        bulletSpeed = GenerateConfig(finalRarity, bulletSpeedUpper, bulletSpeedLower);
        bulletSpread =(int) GenerateConfig(finalRarity, bulletSpreadUpper, bulletSpreadLower,true);
        Debug.Log("Generated full gun config");
    }

    public string GenerateDesc()
    {
       return 
            $"Fire Cooldown: {MathF.Round(fireRate, 2)}{Environment.NewLine}" +
            $"Range: {MathF.Round(fireRange, 2)}{Environment.NewLine}" +
            $"Damage: {MathF.Round(damage)}{Environment.NewLine}" +
            $"Lifesteal: {MathF.Round(lifesteal, 2)*100}{Environment.NewLine}%" +
            $"Bullet Speed: {MathF.Round(bulletSpeed, 2)}{Environment.NewLine}" +
            $"Piercing: {piercing}{Environment.NewLine}" +
            $"Spread: \u00b1{MathF.Round(bulletSpread, 2)}deg";
        
    }
}
