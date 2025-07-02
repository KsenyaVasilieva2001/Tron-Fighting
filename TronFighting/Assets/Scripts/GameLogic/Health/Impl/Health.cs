using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private float _maxHealth = 100f;
    public float CurrentHealth { get; private set; }
    public float MaxHealth => _maxHealth;

    public event Action OnDeath;
    public event Action<float> OnHealthChanged;


    void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void Heal(float hp)
    {
        CurrentHealth += hp;
        if (CurrentHealth > _maxHealth)
            CurrentHealth = _maxHealth;

        OnHealthChanged?.Invoke(CurrentHealth);
    }

    public void TakeDamage(float hp)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= hp;
        OnHealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnDeath?.Invoke();
        }
    }
}
