using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBonus : MonoBehaviour
{
    [SerializeField] private float healAmount = 20f;

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();
        if (health != null)
        {
            health.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}