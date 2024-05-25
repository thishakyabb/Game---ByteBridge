using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Gun Config")]
public class GunConfig : ScriptableObject
{
    [SerializeField]public float fireRange = 10f;
    [SerializeField] public GameObject projectile;
    [SerializeField] public float fireRate = 0.5f;
    [SerializeField]
    public Sprite gunSprite;

    public int damage = 25;
    [SerializeField] public int bulletCount = 1;
    [Range(0,35)] [SerializeField] public int bulletSpread = 0;
    [SerializeField] public float bulletSpeed = 20f;
}
