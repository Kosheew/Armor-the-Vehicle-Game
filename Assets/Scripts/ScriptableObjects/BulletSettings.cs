using UnityEngine;

[CreateAssetMenu(fileName = "BulletSettings", menuName = "Settings/BulletSettings")]
public class BulletSettings : ScriptableObject
{
    [SerializeField, Tooltip("Bullet flight speed")]
    private float bulletSpeed = 25f;

    [SerializeField, Tooltip("Damage dealt by the bullet")]
    private int damage = 20;

    [SerializeField, Tooltip("Time before the bullet returns to the pool")]
    private float returnTimePool = 3f;

    public float BulletSpeed => bulletSpeed;
    public int Damage => damage;
    public float ReturnTimePool => returnTimePool;
}