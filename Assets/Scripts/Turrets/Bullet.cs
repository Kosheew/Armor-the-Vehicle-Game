using Handlers.Damage;
using UnityEngine;
using ObjectPool;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private BulletSettings bulletSettings;
    
    private Rigidbody _rb;
    private CustomPool<Bullet> _objectPool;
    private bool _isReleased;
    private IDamageHandler _damageHandler;
    
    public void SetPool<T>(CustomPool<T> pool) where T : Component
    {
        if (_objectPool != null) return; 
        _rb = GetComponent<Rigidbody>();
        _damageHandler = new DamageHandler(bulletSettings.Damage);
        _objectPool = pool as CustomPool<Bullet>;
    }
    
    public void Launch(Vector3 direction)
    {
        _isReleased = false;
        _rb.velocity = direction * bulletSettings.BulletSpeed;
        
        Invoke(nameof(ReturnToPool), bulletSettings.ReturnTimePool);
    }

    private void ReturnToPool()
    {
        if (_isReleased) return;
        
        _isReleased = true;
        _rb.velocity = Vector3.zero; 
        _objectPool.Release(this);   
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damagable))
        {
            _damageHandler.HandleDamage(damagable);
        }
        ReturnToPool();
    }
    
    private void OnDisable()
    {
        CancelInvoke(); 
    }
}
