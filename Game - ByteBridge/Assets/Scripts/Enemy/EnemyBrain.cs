using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float speed = 2f;
    private int currentHealth;
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
    }

    public void Hit(int damage)
    {   
        Random random = new Random();
        float randomValue = (float)(random.NextDouble() * 100);
        bool isCritical = _playerManager.critStat.StatValue * 100 < randomValue;
        if (isCritical)
        {
            anim.SetTrigger("hit");
            Die();
        }
        else
        {
            currentHealth -= (int) Math.Round(damage * _playerManager.damageModifier.StatValue);
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
        Destroy(gameObject);
    }
}


