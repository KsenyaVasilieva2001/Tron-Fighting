using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Вынести логику анимаций в другой класс
//Вынести логику в Input handler

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    public bool useCharacterForward = false;
    public bool lockToCameraForward = false;
    public float turnSpeed = 10f;
    public float gravity = -9.81f;
    public KeyCode jumpKey = KeyCode.Space;

    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float runSpeed = 3f;

    private CharacterController controller;
    private Animator anim;
    private Camera mainCamera;

    private Vector2 input;
    private Vector3 targetDirection;
    private Quaternion freeRotation;
    private float turnSpeedMultiplier;
    private float speed;
    private float velocity;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        GetInput();
        UpdateSpeed();
       //Jump();
        UpdateTargetDirection();
        UpdateRotation();
        Move();
    }

    private void GetInput()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
    }

    private void UpdateSpeed()
    {
        speed = Mathf.Clamp01(Mathf.Abs(input.x) + Mathf.Abs(input.y));
        speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);
        anim.SetFloat("Speed", speed);
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = -1f;

            if (Input.GetKeyDown(jumpKey))
            {
                verticalVelocity = Mathf.Sqrt(jumpForce * -2f * gravity);
                anim.SetTrigger("Jump");
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void UpdateTargetDirection()
    {
        if (!useCharacterForward)
        {
            turnSpeedMultiplier = 1f;
            Vector3 forward = mainCamera.transform.forward;
            forward.y = 0f;
            Vector3 right = mainCamera.transform.right;
            targetDirection = input.x * right + input.y * forward;
        }
        else
        {
            turnSpeedMultiplier = 0.2f;
            Vector3 forward = transform.forward;
            forward.y = 0f;
            Vector3 right = transform.right;
            targetDirection = input.x * right + Mathf.Abs(input.y) * forward;
        }
    }

    private void UpdateRotation()
    {
        if (targetDirection.magnitude < 0.1f)
            return;

        Vector3 lookDirection = targetDirection.normalized;
        freeRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        float eulerY = Mathf.LerpAngle(transform.eulerAngles.y, freeRotation.eulerAngles.y, turnSpeed * turnSpeedMultiplier * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, eulerY, 0);
    }

    private void Move()
    {
        Vector3 horizontalMove = targetDirection.normalized * runSpeed;
        Vector3 move = new Vector3(horizontalMove.x, verticalVelocity, horizontalMove.z);
        controller.Move(move * Time.deltaTime);
    }
}
