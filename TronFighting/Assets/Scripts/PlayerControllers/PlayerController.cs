using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ICharacterState currentState;

    public MovementState movementState;

    [SerializeField] private float moveSpeed;

    void Start()
    {
        movementState = new MovementState(transform, moveSpeed);
        SwitchState(movementState);
    }

    void Update()
    {
        currentState.Execute();
    }

    public void SwitchState(ICharacterState newState)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
