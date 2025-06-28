using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneEnterChecker : MonoBehaviour
{
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
            OnPlayerEnterProximity();
        }
        else if (!isplayerDetected && isPlayerNear)
        {
            isPlayerNear = false;
            OnPlayerExitProximity();
        }
    }

    void OnPlayerEnterProximity()
    {
        Debug.Log("Игрок вошел в радиус врага");
    }

    void OnPlayerExitProximity()
    {
        Debug.Log("Игрок покинул радиус врага");
    }
}
