using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    Image healthBar;
    public float maxHeath;
    public float HP;

    void Start()
    {
        HP = maxHeath;
    }

    void Update()
    {
        healthBar.fillAmount = HP / maxHeath;
    }
}
