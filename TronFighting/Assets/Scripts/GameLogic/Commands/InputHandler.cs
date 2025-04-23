using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Inputs")]
    public KeyCode heavyKey;
    public KeyCode lightKey;
    public KeyCode kickKey;
    public KeyCode blockKey;

    [Header("Attacks")]
    public Attack heavyAttack;
    public Attack lightAttack;
    public Attack kickAttack;
    public Attack block;


    [SerializeField] private PlayerController playerController;
    void Update()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Debug.Log(inputDirection.magnitude);
        if (inputDirection.magnitude > 0)
        {
            IControllable moveCommand = new MoveCommand(playerController.movementState, inputDirection);
            playerController.animator.SetFloat("Speed", inputDirection.magnitude);
            moveCommand.Execute();

        }
        else
        {
            IControllable moveCommand = new MoveCommand(playerController.movementState, Vector3.zero);
            playerController.animator.SetFloat("Speed", inputDirection.magnitude);
            moveCommand.Execute();
        }
    }
}




