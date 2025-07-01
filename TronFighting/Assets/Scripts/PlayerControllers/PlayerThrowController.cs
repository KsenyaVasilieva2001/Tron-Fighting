using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowController : ThrowControllerBase
{
    [SerializeField] Camera playerCamera;
    private Vector3 mousePosition;
    private Ray cameraRay;
    private Vector3 direction;
    private bool isHolding = false;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isHolding = true;
            anim.SetBool("IsAimToThrow", true);
            ShowTrack();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isHolding = false;
            anim.SetBool("IsAimToThrow", false);
            anim.SetTrigger("Throw");
            pathTracker.Clear();
            //ThrowDisk();
        }
        UpdateFirePointTransform();
    }

    public override void ShowTrack()
    {
        mousePosition = Input.mousePosition;
        cameraRay = playerCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(cameraRay, out RaycastHit hit))
        {
            direction = (hit.point - firePoint.position).normalized;
            RotateTowards(direction);
            path = pathTracker.CalculatePath(firePoint.position, direction, reflections, maxDistance);
            pathTracker.Visualize(path);
        }
    }
}
