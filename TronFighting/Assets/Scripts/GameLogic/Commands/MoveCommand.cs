using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : MonoBehaviour, IControllable
{
    private MovementState movementState;
    private Vector3 direction;

    public MoveCommand(MovementState movementState, Vector3 direction)
    {
        this.movementState = movementState;
        this.direction = direction;
    }

    public void Execute()
    {
        movementState.SetDirection(direction);
    }
}
