using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(PlayerFightController))]
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

       // ЧЧЧ гравитаци€ ЧЧЧ
       if (characterController.isGrounded)
       {
           // при касании земли даЄм небольшой Ђприжимающийї импульс,
           // чтобы точно оставатьс€ на землеs
           verticalVelocity = -2f;
       }
       else
       {
           verticalVelocity -= gravity * Time.deltaTime;
       }

       Vector3 velocity = move.normalized * moveSpeed;
       velocity.y = verticalVelocity;
       characterController.Move(velocity * Time.deltaTime);
   }

}
