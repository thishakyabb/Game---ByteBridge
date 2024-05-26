using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunBase : MonoBehaviour
{
   [Header("Prefabs")] [SerializeField] protected GameObject muzzle;
   [SerializeField] protected Transform muzzlePosition;
   [SerializeField] protected GameObject projectile;
   [SerializeField] protected GameObject gunSpriteHolder;
   [SerializeField] public GunConfig gunConfig;
   protected Transform player;
   protected Vector2 offset;
   private PlayerManager playerManager;

   protected float timeSinceLastShot = 0f;
   protected Transform closestEnemy;
   protected Animator anim;
  
   public void Start()
   {
      anim = GetComponent<Animator>();
      timeSinceLastShot = gunConfig.fireRate;
      player = GameObject.Find("Player").transform;
      gunSpriteHolder.GetComponent<SpriteRenderer>().sprite = gunConfig.gunSprite;
      playerManager = PlayerManager.Instance;
      // anim.runtimeAnimatorController = gunConfig.AnimatorController;
      
   }

   public void Update()
   {
      transform.position = (Vector2)player.position + offset;
      FindClosestEnemy();
      AimAtEnemy();
      Shooting();
   }
  

   public void AimAtEnemy()
   {
      if (closestEnemy != null)
      {
         Vector3 direction = closestEnemy.position - transform.position;
         direction.Normalize();

         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.Euler(0,0,angle);
         transform.position = (Vector2)player.position + offset;
      }
   }
   public void FindClosestEnemy()
   {
      closestEnemy = null;
      float closestDistance = Mathf.Infinity;
      EnemyBrain[] enemies = FindObjectsOfType<EnemyBrain>();
      float finalFireRange = gunConfig.fireRange * playerManager.rangeModifier.StatValue;
      foreach (EnemyBrain enemy in enemies)
      {
         float distance = Vector2.Distance(transform.position, enemy.transform.position);
         if (closestDistance > distance && distance <=finalFireRange)
         {
            closestEnemy = enemy.transform;
         }
      }
   }

   public void Shooting()
   {
      if (closestEnemy == null) return;
      timeSinceLastShot += Time.fixedDeltaTime;
      float finalFireRate = gunConfig.fireRate*(playerManager.fireCooldownModifier.StatValue);
      if (timeSinceLastShot >= finalFireRate)
      {
         Shoot();
         timeSinceLastShot = 0;
      }
   }

   public void Shoot()
   {
      var muzzleGo = Instantiate(muzzle, muzzlePosition.position, transform.rotation);
      muzzleGo.transform.SetParent(transform);
      Destroy(muzzleGo,0.05f);
      anim.SetTrigger("shoot");
      var initialRotation = transform.rotation;
      var rotation = transform.rotation.eulerAngles;
      for (int i = 0; i < gunConfig.bulletCount; i++)
      {
         rotation.z += Random.Range(-gunConfig.bulletSpread, gunConfig.bulletSpread);
         initialRotation.eulerAngles = rotation;
         // rotation.
         var projectileGo = Instantiate(projectile, muzzlePosition.position, initialRotation);
         var bulletScript =  projectileGo.GetComponent<Bullet>();
         bulletScript.damage= gunConfig.damage;
         bulletScript.speed = gunConfig.bulletSpeed;
         Destroy(projectileGo,3);
      }
      
   }

   

   public void SetOffset(Vector2 o)
   {
      offset = o;
   }
}
