using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image playerHealthBar;
    public Image enemyHealthBar;
    public Image enemyHealthBarDamage;
    public Image enemyIcon;
    public TMP_Text timerText;
    public TMP_Text enemyName;

    public float healthSmoothSpeed = 2f;

    private float currentEnemyHealthPercent = 1f;
    private float targetEnemyHealthPercent = 1f;

    private float matchTime = 300f;

    private void Update()
    {
        UpdateTimer();
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        currentEnemyHealthPercent = Mathf.Lerp(currentEnemyHealthPercent, targetEnemyHealthPercent, Time.deltaTime * healthSmoothSpeed);
        enemyHealthBarDamage.fillAmount = currentEnemyHealthPercent;
    }

    private void UpdateTimer()
    {
        matchTime -= Time.deltaTime;
        matchTime = Mathf.Max(matchTime, 0);
        int minutes = Mathf.FloorToInt(matchTime / 60);
        int seconds = Mathf.FloorToInt(matchTime % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void SetEnemyUI(EnemyData data)
    {
        enemyIcon.sprite = data.icon;
        enemyName.text = data.enemyName;
        currentEnemyHealthPercent = 1f;
        targetEnemyHealthPercent = 1f;
        enemyHealthBar.fillAmount = 1f;
        enemyHealthBarDamage.fillAmount = 1f;
    }

    public void UpdateEnemyHealth(int currentHealth, int maxHealth)
    {
        targetEnemyHealthPercent = (float)currentHealth / maxHealth;
        enemyHealthBar.fillAmount = targetEnemyHealthPercent;
    }
}
