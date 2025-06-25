using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] public Transform firePoint;
    public int reflections = 3;
    public float maxDistance = 20f;

    [SerializeField] private PathTracker pathTracker;
    private DiskFactory _diskFactory;
    [SerializeField] private Disk diskPrefab;

    private Vector3 mousePosition;
    private Ray cameraRay;
    private Vector3 direction;
    private Vector3[] path;

    [Header("Hand Bone")]
    [SerializeField] private Transform handBone;
    private bool isHolding = false;

    [SerializeField] private Animator anim;

    private void Start()
    {
        _diskFactory = new DiskFactory(diskPrefab);
    }

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

    public void OnThrowAnimationEnd()
    {
        Debug.Log("HEllo!");
        ThrowDisk();
    }

    private void UpdateFirePointTransform()
    {
        //firePoint.position = handBone.position;
        //firePoint.rotation = handBone.rotation;
    }

    private void ThrowDisk()
    {
        _diskFactory.CreateDisk(path);
        pathTracker.Clear();
    }

    private void ShowTrack()
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
    private void RotateTowards(Vector3 dir)
    {
        Debug.Log("Dir: " + dir);
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f)
            return;
        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            720f * Time.deltaTime
        );
    }

}
