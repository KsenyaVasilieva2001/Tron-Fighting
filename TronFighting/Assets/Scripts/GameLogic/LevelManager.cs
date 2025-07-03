using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Health enemyHealth;
    [SerializeField] private GameObject levelEndPrefab;
    [SerializeField] private Transform levelEndSpawnPoint;
    [SerializeField] private EndGameController endGamePanel;


    private GameObject _spawnedLevelEnd;

    private void Start()
    {
        enemyHealth.OnDeath += HandleEnemyDeath;
        playerHealth.OnDeath += HandlePlayerDeath;
    }

    private void HandleEnemyDeath()
    {
        _spawnedLevelEnd = Instantiate(levelEndPrefab, levelEndSpawnPoint.position, Quaternion.identity);
    }

    private void HandlePlayerDeath()
    {
        endGamePanel.Show("You're lose!");
        PauseGame();
    }

    public void HandlePlayerReachedLevelEnd()
    {
        endGamePanel.Show("You're win!");
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene("Menu");
        ResumeGame();
    }
}
