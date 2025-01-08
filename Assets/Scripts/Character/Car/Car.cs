using UnityEngine;

public class Car : MonoBehaviour, IDamagable
{
    [SerializeField] private Health health;

    public void Inject()
    {
        health = new Health(100);
        health.OnHealthDepleted.AddListener(OnDestroyed);
    }

    private void OnDestroyed()
    {
        Debug.Log("Car destroyed!");
        Destroy(gameObject); // Знищення машини
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }
}