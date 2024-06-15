using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

   private Coroutine ToastTimer;
   private int rerollNumber = 0;
   private int rerollCost = 25;
   private ApiManager ApiManager;
   private bool markRewardCollected = false;
   private bool energyRewardCollected = false;
   private float toastDuration = 5f;
   
   public void Awake()
   {
      if (Instance == null) Instance = this;
   }
   private void Start()
   {
      PlayerManager = PlayerManager.Instance;
      Stats = PlayerManager.GetModifiers();
      UpdateStats();
      StartCoroutine(StaggerRandom());
      ApiManager = ApiManager.Instance;
      StartCoroutine(ApiManager.AuthenticateMockAPI(e => { Debug.Log(e); }));
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
     

      markRewardCollected = true;
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
<<<<<<< HEAD
                           Debug.Log("Got the reward");
=======
                           setToast("Good job on saving energy!");
>>>>>>> f073efec7 (added toast to loadoutUI controller)
                           PlayerManager.coins += 200;
                        }
                        else
                        {
<<<<<<< HEAD
                           Debug.Log("no reward for today");
=======
                           setToast("Energy usage too high! No energy reward today");
>>>>>>> f073efec7 (added toast to loadoutUI controller)
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
         // int randomIndex = random.Next(myList.Count);
         // randomItem = myList[randomIndex];
         var chosenCard = CardPrefabs[Random.Range(0, CardPrefabs.Count)];
         Instantiate(chosenCard, cardHolder.transform);
            

         // Use the randomItem here
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
         // int randomIndex = random.Next(myList.Count);
         // randomItem = myList[randomIndex];
         var chosenCard = CardPrefabs[Random.Range(0, CardPrefabs.Count)];
         Instantiate(chosenCard, cardHolder.transform);
         yield return new WaitForSeconds(0.3f);

         // Use the randomItem here
      }
   }
   

   public void UpdateInventory()
   {
      int i = 0;
      foreach (var gun in PlayerManager.guns)
      {
         inventoryHolder.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = gun.gunSprite;
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
   

   public void ToggleLoadoutUI()
   {
      if (LoadoutUIController.activeSelf)
      {
         LoadoutUIController.SetActive(false);
      }
      else
      {
         LoadoutUIController.SetActive(true);
      }
   }
}
