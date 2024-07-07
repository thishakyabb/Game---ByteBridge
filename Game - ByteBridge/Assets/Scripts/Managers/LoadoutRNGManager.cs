using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadoutRNGManager : MonoBehaviour
{
   private PlayerManager PlayerManager;
   private List<Stat> Stats;
   [SerializeField] private GameObject statsHolder;
   [SerializeField] private GameObject statsGameObject;
   [SerializeField] private List<GameObject> CardPrefabs;
   [SerializeField] private GameObject cardHolder;
   [SerializeField] private GameObject inventoryHolder;
   [SerializeField] private TextMeshProUGUI ToastHolder;
   public static LoadoutRNGManager Instance;
   [SerializeField] private GameObject LoadoutUIController;
   [SerializeField] private TextMeshProUGUI rerollCoinTMP;
   [SerializeField] private TextMeshProUGUI coinTMP;
   [SerializeField] private float rerollCostIncrementPercentage = 25f;
   [SerializeField] private GameObject rewardButton;
   [SerializeField] private Sprite transparentSprite;
   [SerializeField] private AudioClip pop;
   [SerializeField] private AudioClip doublepop;
   [SerializeField] private Sprite invisibleSprite;
   
   //internal private use 
   private Coroutine ToastTimer;
   private int rerollNumber = 0;
   private int rerollCost = 25;
   private ApiManager ApiManager;
   private bool markRewardCollected = false;
   private bool energyRewardCollected = false;
   private float toastDuration = 5f;
   private WeightedRNGCalulator _weightedRngCalulator;
   private int leaderboardPressedCounter = 0;
   
   //singleton interface
   public void Awake()
   {
      if (Instance == null) Instance = this;
   }
   private void Start()
   {
      PlayerManager = PlayerManager.Instance;
      Stats = PlayerManager.GetModifiers();
      UpdateStats();
      ApiManager = ApiManager.Instance;
      StartCoroutine(ApiManager.AuthenticateMockAPI(e => { Debug.Log(e); }));
      
      //safe to run this at start since the loadout screen is the first thing the players see
      SoundManager.Instance.PlayBgMenuMusic(0.5f);
      List<WeightedRNGItem> rngItems = new List<WeightedRNGItem>();
      foreach (var prefab in CardPrefabs)
      {
         float weight = prefab.GetComponent<CardBase>().weight;
         rngItems.Add(new WeightedRNGItem(weight,prefab));
      }
      _weightedRngCalulator = new WeightedRNGCalulator(rngItems);
      StartCoroutine(StaggerRandom());
   }

   public void LoadLeaderboard()
   {
      leaderboardPressedCounter++;
      if (leaderboardPressedCounter >= 2)
      {
         SceneManager.LoadScene(5);
      }
      else
      {
         setToast("Going to leaderboard will quit game. Progress will not be saved. Press again to confirm.");
      }
      
   }

   public void GetRewards()
   {
      
      if (!markRewardCollected)
      {
         StartCoroutine(ApiManager.GetMarks(marks =>
         {
            PlayerManager.coins += marks * 15;
         },"101205780V"));
      }
      GetEnergyReward();
     
      SoundManager.Instance.PlaySoundClip(pop,transform,1f);
      //dont give out rewards for marks again
      markRewardCollected = true;
   }

   public void RemoveGunFromInventory(int slotId)
   {
      PlayerManager.guns.RemoveAt(slotId);
      UpdateInventory();
   }

   public void GetEnergyReward()
   {
      if (!energyRewardCollected)
      {
         StartCoroutine(ApiManager.GetLastMonthAverage(avrg=>
         
               StartCoroutine(ApiManager.GetUsageToday(tdy =>
                     {
                        if (avrg > tdy)
                        {
                           setToast("Good job on saving energy!");
                           PlayerManager.coins += 200;
                           WaveManager.Instance.difficultyModifier -= 0.4f;
                           WaveManager.Instance.difficultyModifierIncrement = 0.2f;
                        }
                        else
                        {
                           setToast("Energy usage too high! No energy reward today");
                        }
                     }
                  )
               )
            )
         );
      }
      else
      {
         setToast("Reward already collected");
      }
      energyRewardCollected = true;
   }

   private void Update()
 {
      coinTMP.text = PlayerManager.coins.ToString();
      rerollCoinTMP.text = rerollCost.ToString();
      
   }

   public void UpdateStats()
   {
      foreach (var stat in Stats)
      {
         var statgo = Instantiate(statsGameObject, statsHolder.transform);
         statgo.GetComponent<StatObj>().SetValues(stat.StatName,stat.StatValue);
      }
   }

   public void Reroll()
   {
      SoundManager.Instance.PlaySoundClip(doublepop,transform,1f);
      if (PlayerManager.coins > rerollCost)
      {
      CardBase[] cards = cardHolder.gameObject.GetComponents<CardBase>();
      foreach (var card in cards)
      {
        Destroy(card.gameObject); 
      }
         StartCoroutine(StaggerRandom());
         PlayerManager.coins -= rerollCost;
         rerollNumber++;
         rerollCost += rerollCost * (int)rerollCostIncrementPercentage / 100;
         
      }
      
   }


   public void GetRandomCards()
   {
      CardBase[] cards = cardHolder.gameObject.GetComponentsInChildren<CardBase>();
      foreach (var card in cards)
      {
        Destroy(card.gameObject); 
      }
      for (int i = 0; i < 4; i++)
      {
         
         var chosenCard = (GameObject)_weightedRngCalulator.GetRandomGO();
         Instantiate(chosenCard, cardHolder.transform);
      }
   }

   public void setToast(string toastText)
   {
      if (ToastTimer != null)
      {
        StopCoroutine(ToastTimer); 
      }
      ToastTimer = StartCoroutine(ToastTimerFunc(toastText));
   }
   private IEnumerator ToastTimerFunc(string toastText)
   {
      ToastHolder.text = toastText; 
      float currentTime = 0f;
      while ( currentTime <= toastDuration)
      {
         currentTime++;
         yield return new WaitForSeconds(1);
      }

      if (currentTime > toastDuration)
      {
         ToastHolder.text = "";
      }
   }

   public IEnumerator StaggerRandom()
   {
      
      CardBase[] cards = cardHolder.gameObject.GetComponentsInChildren<CardBase>();
      foreach (var card in cards)
      {
        Destroy(card.gameObject); 
      }
      for (int i = 0; i < 4; i++)
      {
         var chosenCard = (GameObject)_weightedRngCalulator.GetRandomGO();
         Instantiate(chosenCard, cardHolder.transform);
         yield return new WaitForSeconds(0.3f);

         // Use the randomItem here
      }
   }
   

   public void UpdateInventory()
   {
      for (int j = 0; j < 6; j++)
      {
         var inventorySlot  = inventoryHolder.transform.GetChild(j);
         inventorySlot.transform.GetChild(0).GetComponent<Image>().sprite = invisibleSprite;
      } 
      
      int i = 0;
      foreach (var gun in PlayerManager.guns)
      {
         var inventorySlot  = inventoryHolder.transform.GetChild(i);
         inventorySlot.transform.GetChild(0).GetComponent<Image>().sprite = gun.gunSprite;
         inventorySlot.GetComponent<TooltipTrigger>().content = gun.GenerateDesc();
         inventorySlot.GetComponentInChildren<InventorySlot>().slotId = i; 
         inventorySlot.GetComponentInChildren<InventorySlot>().initialised = true; 
         i++;
      }
      
      
   }
   public void RestartGame()
   {
      markRewardCollected = false;
      energyRewardCollected = false;
      for (int i = 0; i < 6; i++)
      {
         inventoryHolder.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = transparentSprite;
      }
      GetRandomCards();
   }
   

}
