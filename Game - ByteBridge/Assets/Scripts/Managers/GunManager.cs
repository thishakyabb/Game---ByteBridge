using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] private GameObject gunPrefab;
    private Transform player;
    private List<Vector2> gunPositions = new List<Vector2>();
    private int spawnedGuns = 0;
    private PlayerManager PlayerManager;

    public static GunManager Instance;

    public void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        gunPositions.Add(new Vector2(-1.2f,0.2f));
        gunPositions.Add(new Vector2(1.2f,0.2f));
        gunPositions.Add(new Vector2(-1.4f,-0.4f));
        gunPositions.Add(new Vector2(1.4f,-0.4f));
        gunPositions.Add(new Vector2(-1f,-0.9f));
        gunPositions.Add(new Vector2(1f,-0.9f));
        
        PlayerManager = PlayerManager.Instance;

        
    }

    public void SpawnGuns()
    {
        foreach (var gunConfig in PlayerManager.guns)
        {
            AddGun(gunConfig);
        }
    }

    void AddGun(GunConfig gc)
    {
        var pos = gunPositions[spawnedGuns];
        var newGun = Instantiate(gunPrefab, pos, Quaternion.identity);
        var gunscript = newGun.GetComponent<Gun>();
        gunscript.SetOffset(pos);
        gunscript.gunConfig = gc;
        spawnedGuns++;
    }

    public void RemoveAllGuns()
    {
        GunBase[] guns = (GunBase[])GameObject.FindObjectsOfType(typeof(GunBase));
        foreach (var gun in guns)
        {
            Destroy(gun.gameObject);
        }

        spawnedGuns = 0;
    }
}
