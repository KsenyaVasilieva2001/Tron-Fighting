using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FightController))]
public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 10f;
    private FightController input;
    [SerializeField] private Transform playerCameraTransform;

    void Awake()
    {
        input = GetComponent<FightController>();
    }

    public void Rotate()
    {
        Vector3 direction = new Vector3(input.MovementInput.x, 0, input.MovementInput.y);
        if (direction.magnitude < 0.1f) return;

        Vector3 cameraRelativeDir = playerCameraTransform.forward * direction.z + playerCameraTransform.right * direction.x;
        cameraRelativeDir.y = 0f;

        //добавила
        //targetRotation *= Quaternion.Euler(0, -90f, 0);


        Quaternion targetRotation = Quaternion.LookRotation(-cameraRelativeDir);
        //добавила
        targetRotation *= Quaternion.Euler(0, -90f, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
