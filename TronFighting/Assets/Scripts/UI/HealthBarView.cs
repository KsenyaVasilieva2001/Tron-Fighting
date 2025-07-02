using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    public void UpdateHealth(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }
}
