using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private ICharacterState currentState;
    public UIController uiController;
    public Enemy currentEnemy;

    public MovementState movementState;

    [SerializeField] private float moveSpeed;

    void Start()
    {
        movementState = new MovementState(transform, moveSpeed);
        animator = GetComponent<Animator>();
        SwitchState(movementState);
    }

    void Update()
    {
        currentState.Execute();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            currentEnemy = enemy;
            uiController.SetEnemyUI(enemy.data);
        }
    }

    public void SwitchState(ICharacterState newState)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
