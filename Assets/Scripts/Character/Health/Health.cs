using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class Health 
{
    private int maxHealth;
    private int _currentHealth;

    public UnityEvent OnHealthDepleted;
    public UnityEvent OnHealthChanged;
    
    public Health(int health)
    {
        maxHealth = health;
        _currentHealth = maxHealth;
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
        _currentHealth = maxHealth;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
    
}
