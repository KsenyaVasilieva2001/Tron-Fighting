using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : MonoBehaviour, ICharacterState
{
    [SerializeField] private Transform characterTransform; 
    [SerializeField] private float speed;
    private Vector3 direction;

    public MovementState(Transform transfrom, float speed)
    {
        this.characterTransform = transfrom;
        this.speed = speed;
    }
    public void Enter()
    {
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }

    public void Execute()
    {
        characterTransform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void Exit()
    {
        direction = Vector3.zero;
    }
}
