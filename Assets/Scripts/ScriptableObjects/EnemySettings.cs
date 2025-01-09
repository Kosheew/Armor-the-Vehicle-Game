using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Settings/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    [SerializeField, Tooltip("Particle effect triggered when the enemy is hit")]
    private ParticleSystem particleHit;

    [SerializeField, Tooltip("Damage dealt by the enemy")]
    private int damage;

    [SerializeField, Tooltip("Speed of the enemy")]
    private float speed;

    [SerializeField, Tooltip("Health of the enemy")]
    private int health;

    public ParticleSystem ParticleHit => particleHit;
    public int Damage => damage;
    public float Speed => speed;
    public int Health => health;
}