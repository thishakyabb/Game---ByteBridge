using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 25;
    public int piercing = 0;
    private int hitEnemies = 0;
    public float lifesteal = 0;    
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.gameObject.GetComponent<EnemyBrain>();
        if (enemy != null)
        {
            if (hitEnemies >= piercing)
            {
                Destroy(gameObject);
            }

            if (lifesteal > 0)
            {
                var stolenLife = Mathf.RoundToInt(lifesteal * enemy.currentHealth); 
                PopupStatManager.Instance.LifeStealStat("Lifesteal +"+stolenLife.ToString(),transform,GetInstanceID().ToString());
                PlayerManager.Instance.currentHealth +=stolenLife;
            }
            hitEnemies++;
            enemy.Hit(damage);
        }
    }
}
