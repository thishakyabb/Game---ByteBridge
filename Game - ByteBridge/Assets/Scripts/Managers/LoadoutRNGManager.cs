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
   public static LoadoutRNGManager Instance;
   [SerializeField] private GameObject LoadoutUIController;
   [SerializeField] private TextMeshProUGUI rerollCoinTMP;
   [SerializeField] private TextMeshProUGUI coinTMP;
   [SerializeField] private float rerollCostIncrementPercentage = 25f;
   [SerializeField] private GameObject rewardButton;
   [SerializeField] private Sprite transparentSprite;
   private int rerollNumber = 0;
   private int rerollCost = 25;
   private ApiManager ApiManager;
   private bool markRewardCollected = false;
   private bool energyRewardCollected = false;
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
                        Debug.Log(avrg);
                        Debug.Log(tdy);
                        if (avrg > tdy)
                        {
                           PlayerManager.coins += 200;
                        }
                     }
                  )
               )
            )
         );
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
      for (int i = cardHolder.transform.childCount - 1; i >= 0; i--)
      {
         Destroy(cardHolder.transform.GetChild(i).gameObject);
      }
         // StartCoroutine(StaggerRandom());
         PlayerManager.coins -= rerollCost;
         rerollNumber++;
         rerollCost += rerollCost * (int)rerollCostIncrementPercentage / 100;
      GetRandomCards();
         
      }
      
   }


   public void GetRandomCards()
   {
        
      for (int i = 0; i < 4; i++)
      {
         // int randomIndex = random.Next(myList.Count);
         // randomItem = myList[randomIndex];
         var chosenCard = CardPrefabs[Random.Range(0, CardPrefabs.Count)];
         Instantiate(chosenCard, cardHolder.transform);
            

         // Use the randomItem here
      }
   }

   public IEnumerator StaggerRandom()
   {
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
