using UnityEngine;

[CreateAssetMenu(fileName = "CarSettings", menuName = "Settings/CarSettings")]
public class CarSettings : ScriptableObject
{
    [SerializeField, Tooltip("Car's health")]
    private int health = 100;

    [SerializeField, Tooltip("Particle effect triggered when the car is destroyed")]
    private ParticleSystem particleDie;

    public int Health => health;
    public ParticleSystem ParticleDie => particleDie;
}