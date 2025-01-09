using Handlers.Damage;
using Character.Health;
using ObjectPool;
using UnityEngine;
using Handlers.Animation;
using UnityEngine.Serialization;
using View;

namespace Character.Enemy
{
    [RequireComponent(typeof(Rigidbody), typeof(Animator))]
    public class Enemy : MonoBehaviour, IDamageable, IPoolable, IUpdatable
    {
        [SerializeField] private EnemySettings enemySettings;
        [SerializeField] private HealthView healthView;
        [SerializeField] private Transform model;
        
        private float _speedChange;
        
        private CustomPool<Enemy> _objectPool;
        private HealthModel _healthModel; 
        private Rigidbody _rb;
        private Animator _animator;
        private Transform _targetPosition;
        private ParticleSystem _particles;
        
        private IDamageHandler _damageHandler;
        private ITargetHandler _targetHandler;
        private IAnimationHandler<float> _speedAnimationHandler;
        private IAnimationResetHandler _speedResetHandler;
        
        public void SetPool<T>(CustomPool<T> pool) where T : Component
        {
            if (_objectPool != null) return;
            _objectPool = pool as CustomPool<Enemy>;
        }

        public void Inject(DependencyContainer container)
        {
            _targetHandler = container.Resolve<ITargetHandler>();
            _speedAnimationHandler = container.Resolve<IAnimationHandler<float>>();
            _speedResetHandler = container.Resolve<IAnimationResetHandler>();
            
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            
            _damageHandler = new DamageHandler(enemySettings.Damage);
            _healthModel = new HealthModel(enemySettings.Health);

            _particles = Instantiate(enemySettings.ParticleHit,transform.position + new Vector3(0, 1.5f, 0),Quaternion.identity,transform);
            
            _healthModel.OnHealthChanged += UpdateHealthView;
            _healthModel.OnHealthDepleted += OnDeath;
            
            _targetPosition = _targetHandler.TargetPosition;
            SetHealthView();
        }
        
        public void ResetEnemy()
        {
            healthView.gameObject.SetActive(false);
            _healthModel.ResetHealth(); 
            _speedResetHandler.ResetAnimation(_animator);
            _speedChange = 0;
            _rb.velocity = Vector3.zero;
            UpdateHealthView();
        }

        private void OnDeath()
        {
            ResetEnemy();
            _objectPool.Release(this);
        }

        private void SetHealthView()
        {
            healthView.SetHealth(_healthModel.GetCurrentHealth());
            healthView.gameObject.SetActive(false);
        }
        private void UpdateHealthView()
        {
            healthView.UpdateHealth(_healthModel.GetCurrentHealth());
        }
    
        public void TakeDamage(int damage)
        {
            _healthModel.TakeDamage(damage);
            _particles.Play();
            healthView.gameObject.SetActive(true);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damagable))
            {
                _damageHandler.HandleDamage(damagable);
                _healthModel.OnHealthDepleted?.Invoke();
            }
        }
    
        public void UpdateObject()
        {
            MoveEnemy();
            _speedAnimationHandler.UpdateAnimation(_animator, _speedChange);
        }
        
        private void MoveEnemy()
        {
            Vector3 direction;
            
            if (Vector3.Distance(transform.position, _targetPosition.position) <= 15 &&
                _targetHandler.TargetAlive)
            {
                direction = (_targetPosition.position - transform.position).normalized;
                _speedChange = Mathf.Lerp(_speedChange, enemySettings.Speed, Time.deltaTime * 15f);
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                model.rotation = Quaternion.Lerp(model.rotation, targetRotation, Time.deltaTime * 15f);
            }
            else
            {
                direction = Vector3.back;
            }
            
            direction *= enemySettings.Speed;
            _rb.velocity = new Vector3(direction.x, _rb.velocity.y, direction.z);
        }

    }
}