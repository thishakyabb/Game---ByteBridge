using System;
using Unity.VisualScripting;
using UnityEngine;

public class Melee: MonoBehaviour
{
    public MeleeScriptableObject meleeConfig;
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        var enemy = other.gameObject.GetComponent<EnemyBrain>();
        if (enemy != null)
        {
            Destroy(gameObject);
            enemy.Hit(meleeConfig.damage);
        }
    }
}
