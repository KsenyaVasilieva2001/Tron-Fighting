using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(InputHandler))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = 9.81f;
    private float verticalVelocity = 0f;


    private CharacterController characterController;
    [SerializeField] private Transform playerCameraTransform;
    private InputHandler input;
    [SerializeField] private Animator animator;
    [SerializeField] private float posY = 0;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        input = GetComponent<InputHandler>();
        animator = GetComponent<Animator>();
    }
    public void Move()
    {
        Vector3 move = new Vector3(input.MovementInput.x, 0, input.MovementInput.y);
        move = playerCameraTransform.forward * move.z + playerCameraTransform.right * move.x;
        move.y = 0;
        characterController.Move(move.normalized * moveSpeed * Time.deltaTime);
    }
}
