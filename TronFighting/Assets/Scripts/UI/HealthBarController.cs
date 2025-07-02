using System.Collections;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private HealthBarView healthBarView;
    [SerializeField] private Health health;

    private void Start()
    {
        health.OnHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float current)
    {
        healthBarView.UpdateHealth(current, health.MaxHealth);
    }
}