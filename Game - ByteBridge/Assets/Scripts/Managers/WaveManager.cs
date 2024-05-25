using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    public EnemyManager EnemyManager;
    public LoadoutRNGManager LoadoutRngManager;
    private GunManager gm;
    public int waveNumber=1;
    [SerializeField] private GameObject LoadoutUI;
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private TextMeshProUGUI timeLeftTMP;
    [SerializeField] private TextMeshProUGUI waveCounterTMP;
    public float difficultyModifier = 1f;
    public float difficultyModifierIncrement = 0.3f;
    public float spawnTimeDecrement = 0.02f;
    public int waveDuration = 60;
    public int timeLeft;
    public void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        EnemyManager = EnemyManager.Instance;
        LoadoutRngManager = LoadoutRNGManager.Instance;
        EnemyManager.spawning = false;
        gm = GunManager.Instance;
        
    }

    private void Update()
    {
        timeLeftTMP.text = String.Format("{0}s",timeLeft);
    }


    public void StartNextWave()
    {
        waveNumber++;
        LoadoutUI.SetActive(false);
        PlayerUI.SetActive(true);
        EnemyManager.spawning = true;
        difficultyModifier += difficultyModifierIncrement;
        EnemyManager.timeBetweenSpawns -= spawnTimeDecrement;
        gm.SpawnGuns();
        waveCounterTMP.text = String.Format("{0}th wave",waveNumber);
        timeLeft = waveDuration;
        StartCoroutine(Countdown());
    }

    public void EndWave()
    {
        
        EnemyManager.spawning = false;
        EnemyManager.DestroyAllEnemies();
        LoadoutUI.SetActive(true);
        PlayerUI.SetActive(false);
        LoadoutRngManager.Reroll();
        gm.RemoveAllGuns();
    }
    private IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }

        if (timeLeft == 0)
        {
            EndWave();
        }

    }
    
    
}
