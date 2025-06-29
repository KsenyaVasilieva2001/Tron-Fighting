using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerInStartFightRange : Node
{
    private static LayerMask playerLayer;

    private Transform _transform;
    private Animator _animator;

    public event Action OnActivateThrow;
    public event Action OnDeactivateThrow;
    public event Action OnActivateFight;
    public event Action OnDeactivateFight;

    private bool isPlayerNear;
    private bool isplayerDetected;

    public CheckPlayerInStartFightRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
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
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
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