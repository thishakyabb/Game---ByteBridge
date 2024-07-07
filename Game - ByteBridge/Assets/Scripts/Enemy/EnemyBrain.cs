using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] public float speed = 2f;
    [SerializeField] private bool canThrowProjectiles = false;
    [SerializeField] private float projectileCooldown = 0.05f;
    [SerializeField] private GameObject projectile;
    public int currentHealth;
    private Transform player;
    private float timeSinceLastShot = 0f;
    private Animator anim;
    private Transform target;
    private PlayerManager _playerManager;
    // private bool moving = false;
    
    private void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        _playerManager = PlayerManager.Instance;
        player = GameObject.Find("Player").transform;
            
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            transform.position += direction * (speed * Time.fixedDeltaTime);
        }

        var playertoright = target.position.x > transform.position.x;
        transform.localScale = new Vector2(playertoright ? -1 : 1, 1);
        if (canThrowProjectiles)
        {
           Shooting(); 
        }
    }

    private void Shoot()
    {
        var player = GameObject.Find("Player").transform;
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(0,0,angle);
        var projectileGo = Instantiate(projectile, transform.position, rotation);
    }

    private void Shooting()
    {
      timeSinceLastShot += Time.fixedDeltaTime;
      if (timeSinceLastShot >= projectileCooldown)
      {
          Debug.Log("shot");
         Shoot();
         timeSinceLastShot = 0;
      }
    }

    public void Hit(int damage)
    {   
        Random random = new Random();
        float randomValue = (float)(random.NextDouble() * 100);
        bool isCritical = _playerManager.critStat.StatValue * 100 > randomValue;
        if (isCritical)
        {
            PopupStatManager.Instance.CritStat("critical",transform,GetInstanceID().ToString());
            anim.SetTrigger("hit");
            Die();
        }
        else
        {
            int d =(int) Math.Round(damage * _playerManager.damageModifier.StatValue);
            currentHealth -= d;
            PopupStatManager.Instance.DamageStat(d.ToString(),transform,GetInstanceID().ToString());
            anim.SetTrigger("hit");
            if (currentHealth <= 0)
            {
                Die();
            }
            
        }
    }

    private void Die()
    {
        transform.parent.GetComponent<EnemyHolder>().lastPosition = transform.position;
        _playerManager.kills++;
        Destroy(gameObject);
    }
}


