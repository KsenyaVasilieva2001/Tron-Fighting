using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "FightingGame/EnemyData")]
public class EnemyData : ScriptableObject
{
    public Sprite icon;
    public string enemyName;
    public int maxHealth;
}
