using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 25;
    
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            Destroy(gameObject);
            player.Hit(damage);
        }
    }
}
