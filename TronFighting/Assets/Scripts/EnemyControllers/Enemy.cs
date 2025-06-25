using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public int currentHealth;
    public Collider collider;

    private void Start()
    {
        currentHealth = data.maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, data.maxHealth);
    }
}
