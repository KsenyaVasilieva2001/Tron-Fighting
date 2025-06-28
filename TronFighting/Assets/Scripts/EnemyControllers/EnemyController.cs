using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static event Action OnActivateThrow;
    public static event Action OnDeactivateThrow;
    public static event Action OnActivateFight;
    public static event Action OnDeactivateFight;

    private bool isPlayerNear;
    private bool isplayerDetected;

    [SerializeField] private float areaRadius;
    [SerializeField] private LayerMask playerLayer;

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, areaRadius, playerLayer);
        isplayerDetected = hits.Length > 0;
        if (isplayerDetected && !isPlayerNear)
        {
            isPlayerNear = true;
            OnPlayerEnterFightZone();
        }
        else if (!isplayerDetected && isPlayerNear)
        {
            isPlayerNear = false;
            OnPlayerExitFightZone();
        }
    }

    void OnPlayerEnterFightZone()
    {
        Debug.Log("Игрок вошел в радиус врага");
        OnActivateFight?.Invoke();
        OnDeactivateThrow?.Invoke();
    }

    void OnPlayerExitFightZone()
    {
        Debug.Log("Игрок покинул радиус врага");
        OnActivateThrow?.Invoke();
        OnDeactivateFight?.Invoke();
    }
}
