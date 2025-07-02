using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerInStartFightRange : Node
{
    private static LayerMask playerLayer;
    private EnemyBT _enemy;

    private Transform _transform;

    private bool isPlayerNear;

    public CheckPlayerInStartFightRange(EnemyBT enemy)
    {
        _enemy = enemy;
        _transform = enemy.transform;
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
                    
                    _enemy.OnPlayerEnterFightZone();
                }

                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                if (isPlayerNear)
                {
                    isPlayerNear = false;
                    _enemy.OnPlayerExitFightZone();
                }
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
                    _enemy.OnPlayerExitFightZone();
                }

                state = NodeState.FAILURE;
                return state;
            }
            else
            {
                if (!isPlayerNear)
                {
                    isPlayerNear = true;
                    _enemy.OnPlayerEnterFightZone();
                }
            }
        }

        state = NodeState.SUCCESS;
        return state;
    }
}