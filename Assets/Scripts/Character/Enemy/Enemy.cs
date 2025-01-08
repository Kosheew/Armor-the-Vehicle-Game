using Character.Damage;
using ObjectPool;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable, IPoolable
{
    [SerializeField] private HealthView healthView;
    [SerializeField] private int damage = 15;
    [SerializeField] private Health health = new Health(100); 
    
    private CustomPool<Enemy> _objectPool;
    private IDamageHandler _damageHandler;
    
    public void SetPool<T>(CustomPool<T> pool) where T : Component
    {
        if (_objectPool != null) return;
        _damageHandler = new DamageHandler(damage);
        _objectPool = pool as CustomPool<Enemy>;
        health.OnHealthDepleted.AddListener(OnDeath);
        health.OnHealthChanged.AddListener(UpdateHealthView);
    }
    
    public void ResetEnemy()
    {
        health.ResetHealth(); 
        UpdateHealthView();
    }

    private void OnDeath()
    {
        health.ResetHealth();
        _objectPool.Release(this);
        Debug.Log("Die");
    }

    private void UpdateHealthView()
    {
        healthView.UpdateHealth(health.GetCurrentHealth());
    }
    
    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            _damageHandler.HandleDamage(damagable);
            health.OnHealthDepleted?.Invoke();
        }
    }
}