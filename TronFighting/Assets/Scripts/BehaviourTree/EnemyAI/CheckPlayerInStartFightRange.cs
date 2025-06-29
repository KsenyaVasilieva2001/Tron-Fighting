using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerInStartFightRange : Node
{
    private static LayerMask playerLayer;

    private Transform _transform;

    public event Action OnActivateThrow;
    public event Action OnDeactivateThrow;
    public event Action OnActivateFight;
    public event Action OnDeactivateFight;

    private bool isPlayerNear;
    private bool isplayerDetected;

    public CheckPlayerInStartFightRange(Transform transform)
    {
        _transform = transform;
        playerLayer = LayerMask.GetMask("Player");
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");

        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(
                _transform.position, EnemyBT.startFightRange, playerLayer);

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                if (!isPlayerNear)
                {
                    isPlayerNear = true;
                    OnPlayerEnterFightZone();
                }
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                if (isPlayerNear)
                {
                    isPlayerNear = false;
                    OnPlayerExitFightZone();
                }
                state = NodeState.FAILURE;
                return state;
            }
        }
        else
        {
            Transform player = (Transform)t;
            float distance = Vector3.Distance(_transform.position, player.position);

            if (distance > EnemyBT.startFightRange)
            {
                parent.parent.ClearData("target");

                if (isPlayerNear)
                {
                    isPlayerNear = false;
                    OnPlayerExitFightZone();
                }

                state = NodeState.FAILURE;
                return state;
            }
            else
            {
                if (!isPlayerNear)
                {
                    isPlayerNear = true;
                    OnPlayerEnterFightZone();
                }

                state = NodeState.SUCCESS;
                return state;
            }
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