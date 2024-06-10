using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int currentHealth;
    // basics
    public bool dead = false;
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public int maxHealth = 10;
    [SerializeField] public float luck = 1f;
    [SerializeField] public List<GunConfig> guns;
    // modifiers all percentage values 
    [FormerlySerializedAs("fireRateModifier")] [SerializeField] public Stat fireCooldownModifier = new Stat(1f,"Fire Rate");
    [SerializeField] public Stat critStat = new Stat(0f,"Crit Percentage");
    [SerializeField] public Stat movementSpeedModifier = new Stat(1f,"Move Speed");
    [SerializeField] public Stat maxHealthModifier = new Stat(1f,"Crit Percentage");
    [SerializeField] public Stat luckModifier = new Stat(1f,"Luck");
    [SerializeField] public Stat rangeModifier = new Stat(1f,"Range");
    [SerializeField] public Stat damageModifier = new Stat(1f,"Damage");
    [SerializeField] public Stat regenModifier = new Stat(0f,"Regeneration");
     
    [Header("UI elements")]
    [SerializeField] private TextMeshProUGUI healthTextTMP;
    [SerializeField] private Image healthFill;
    [SerializeField] private TextMeshProUGUI coinTextTMP;
    
    public static PlayerManager Instance;
    private LoadoutRNGManager LoadoutRngManager;
    
    public int kills = 0;

    private int _coins;
    public int coins
    {
        get
        {
            return _coins;
        }
        set
        {
            Debug.Log("got set here");
            _coins = value;
        }
    }
    public int bestWave = 0;
    public void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        LoadoutRngManager = LoadoutRNGManager.Instance;
        currentHealth = maxHealth;
        UpdateHealth();
        UpdateCoins();
        
    }

    private void Update()
    {
        UpdateCoins();
        UpdateHealth();
    }

    public List<Stat> GetModifiers()
    {
        List<Stat> list = new List<Stat>
        {
            fireCooldownModifier,
            critStat,
            movementSpeedModifier,
            maxHealthModifier,
            luckModifier,
            rangeModifier,
            damageModifier,
            regenModifier
        };
        return list;
    }

    public void AddGuns(GunConfig gc)
    {
        if (guns.Count >= 6)
        {
            guns.RemoveAt(guns.Count - 1);
        }
        guns.Add(gc);
        LoadoutRngManager.UpdateInventory();
    }

    public void UpdateCoins()
    {
        coinTextTMP.text = coins.ToString();
    }

    public void UpdateHealth()
    {
        
        healthTextTMP.text = String.Format("{0}/{1}",Mathf.Clamp(currentHealth, 0, maxHealth).ToString(),maxHealth);
        healthFill.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void ResetStats()
    {
        guns.Clear();
        coins = 0;
        dead = false;

        
       fireCooldownModifier.StatValue = 1f;
       critStat.StatValue = 0f;
       movementSpeedModifier.StatValue = 1f;
       maxHealthModifier.StatValue = 1f;
       luckModifier.StatValue = 1f;
       rangeModifier.StatValue = 1f;
       damageModifier.StatValue = 1f;
       maxHealth = 100;
       currentHealth = maxHealth;
       regenModifier.StatValue = 0f;
       
       
       kills = 0;
       bestWave = 0;
    }
    
}
