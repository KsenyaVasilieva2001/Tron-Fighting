using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    private LevelManager _levelManager;

    public void Init(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }


    private void OnColliderEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _levelManager.HandlePlayerReachedLevelEnd();
            gameObject.SetActive(false);
        }
    }
}