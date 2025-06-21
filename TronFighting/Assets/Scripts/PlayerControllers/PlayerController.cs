using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public UIController uiController;
    public Enemy currentEnemy;

    private PlayerMovement movement;
    private PlayerRotation rotation;

    //  public MovementState movementState;

    [SerializeField] private float moveSpeed;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        rotation = GetComponent<PlayerRotation>();
    }

    void Start()
    {
        // movementState = new MovementState(transform, moveSpeed);
        animator = GetComponent<Animator>();
        // SwitchState(movementState);
    }

    void Update()
    {
        movement.Move();
        rotation.Rotate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            currentEnemy = enemy;
            uiController.SetEnemyUI(enemy.data);
        }
    }
}
