using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour, IDamage
{
    public void CauseDamage(IHealth target, float amount)
    {
        target.TakeDamage(amount);
    }
}
