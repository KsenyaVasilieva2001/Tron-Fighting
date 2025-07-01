using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActionThrowDisk : Node
{
    private EnemyThrowController _throwController;
    private float throwInterval = 3f;
    private float _timer;

    public ActionThrowDisk(EnemyThrowController throwController)
    {
        _throwController = throwController;
    }
    public override NodeState Evaluate()
    {
        if (!_throwController.IsActive())
        {
            state = NodeState.FAILURE;
            return state;
        }

        _timer += Time.deltaTime;
        _throwController.ShowTrack();

        if (_timer >= throwInterval)
        {
            _timer = 0f;
            _throwController.StartThrow();
        }

        state = NodeState.RUNNING;
        return state;
    }
}