using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 6f;
    [SerializeField] private Vector2 offset = new Vector2(0, 3f);
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;

    private bool isOverlapped = true;
    private float yaw;
    private float pitch;

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 direction = rotation * new Vector3(0, 0, -distance);
        Vector3 desiredPosition = target.position + Vector3.up * offset.y + Vector3.right * offset.x + direction;

        isOverlapped = CheckOverviewOverlap(target.position, desiredPosition);
        if (!isOverlapped) transform.position = desiredPosition;

        transform.LookAt(target.position + Vector3.up * offset.y);
    }

    private bool CheckOverviewOverlap(Vector3 player, Vector3 desiredCamPos)
    {
        return Physics.Linecast(player, desiredCamPos);
    }
}