using System;

namespace Character.Health
{
    public class HealthModel 
    {
        private readonly int _maxHealth;
        private int _currentHealth;

        public Action OnHealthDepleted;
        public event Action OnHealthChanged;
    
        public HealthModel(int health)
        {
            _maxHealth = health;
            _currentHealth = _maxHealth;
        }
    
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
        
            OnHealthChanged?.Invoke();
        
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnHealthDepleted?.Invoke();
            }
        }
    
        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }

        public int GetCurrentHealth()
        {
            return _currentHealth;
        }
    
    }
}
