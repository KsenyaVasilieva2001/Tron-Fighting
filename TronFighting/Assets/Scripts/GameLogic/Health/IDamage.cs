using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public void CauseDamage(IHealth target, float amount);
}
