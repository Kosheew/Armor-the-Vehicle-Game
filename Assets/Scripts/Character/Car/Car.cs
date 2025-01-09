using Character.Health;
using UnityEngine;
using Handlers.Enemies;
using Handlers.Damage;
using Handlers.Animation;
using Unity.Mathematics;
using View;

namespace Character.Car
{
    public class Car : MonoBehaviour, IDamageable, ITargetHandler
    {
        [SerializeField] private CarSettings carSettings;
        [SerializeField] private HealthView healthView;
        [SerializeField] private GameObject car;
        
        private Animator _animator;
        private ParticleSystem _particleDie;
        
        private IAnimationHandler<bool> _damageAnimationHandler;
        private HealthModel _healthModel;
        
        public bool TargetAlive { get; private set; }
        public Transform TargetPosition => transform;
        
        public void Inject(DependencyContainer container)
        {
            _animator = GetComponent<Animator>();
            _damageAnimationHandler = container.Resolve<IAnimationHandler<bool>>();
            
            _healthModel = new HealthModel(carSettings.Health);
            
            _particleDie = Instantiate(carSettings.ParticleDie, transform.position, Quaternion.identity, transform );
            
            _healthModel.OnHealthDepleted += OnDeath;
            _healthModel.OnHealthChanged += UpdateHealthView;
            
            TargetAlive = true;
            
            healthView.SetHealth(_healthModel.GetCurrentHealth());
        }

        private void OnDeath()
        {
            TargetAlive = false;
            _particleDie.Play();
            car.SetActive(false);
        }

        private void UpdateHealthView()
        {
            healthView.UpdateHealth(_healthModel.GetCurrentHealth());
        }
        
        public void TakeDamage(int damage)
        {
            _healthModel.TakeDamage(damage);
            _damageAnimationHandler.UpdateAnimation(_animator, true);
        }
    }
}