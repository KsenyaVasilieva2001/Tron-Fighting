using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bonusPrefab;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private Transform[] spawnPoints;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            SpawnBonus();
            _timer = 0f;
        }
    }

    private void SpawnBonus()
    {
        if (spawnPoints.Length == 0) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(bonusPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }
}
