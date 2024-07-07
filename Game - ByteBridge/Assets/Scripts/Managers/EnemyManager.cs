using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] public float timeBetweenSpawns = 1f;
    [SerializeField] private GameObject enemyHolder;
    private float currentTimeBetweenSpawns;
    public bool spawning;
    private WeightedRNGCalulator _weightedRngCalulator;

    private Transform enemiesParent;
    public static EnemyManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        enemiesParent = GameObject.Find("Enemies").transform;
        List<WeightedRNGItem> weightedEnemyList = new List<WeightedRNGItem>();
        foreach (var enemy in enemies)
        {
            float weight = enemy.GetComponent<WeightMono>().weight;
            weightedEnemyList.Add(new WeightedRNGItem(weight,enemy));
        }

        _weightedRngCalulator = new WeightedRNGCalulator(weightedEnemyList);
    }

    private void Update()
    {
        currentTimeBetweenSpawns -= Time.fixedDeltaTime;
        if (currentTimeBetweenSpawns <= 0 && spawning)
        {
            SpawnEnemy();
            currentTimeBetweenSpawns = timeBetweenSpawns;
        }
    }

    private Vector2 RandomPosition()
    {
        // Vector2 randomDirection  = Random.insideUnitCircle
        return new Vector2(Random.Range(-16, 16), Random.Range(-8,8));
    }

    private void SpawnEnemy()
    {
        var enemyPrefab = _weightedRngCalulator.GetRandomGO();
        var e = Instantiate(enemyHolder, RandomPosition(), Quaternion.identity);
        e.GetComponent<EnemyHolder>().enemyPrefab = enemyPrefab;
        e.transform.SetParent(enemiesParent);
    }

    public void DestroyAllEnemies()
    {
        foreach (Transform e in enemiesParent) 
            Destroy(e.gameObject);
    }
}
