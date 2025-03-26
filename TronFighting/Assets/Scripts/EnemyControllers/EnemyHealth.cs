using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;

    public void React()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy is Damaged!");
    }

   public void Die()
   {

   }
}
