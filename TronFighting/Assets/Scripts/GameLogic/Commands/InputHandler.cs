using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    void Update()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (inputDirection.magnitude > 0)
        {
            IControllable moveCommand = new MoveCommand(playerController.movementState, inputDirection);
            moveCommand.Execute();
        }
        else
        {
            IControllable moveCommand = new MoveCommand(playerController.movementState, Vector3.zero);
            moveCommand.Execute();
        }
    }
}
