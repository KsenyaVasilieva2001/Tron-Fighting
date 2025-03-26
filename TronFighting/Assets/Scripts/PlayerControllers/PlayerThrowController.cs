using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowController : MonoBehaviour
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

    private void Start()
    {
        _diskFactory = new DiskFactory(diskPrefab);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ShowTrack();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ThrowDisk();
        }
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
            path = pathTracker.CalculatePath(firePoint.position, direction, reflections, maxDistance);
            pathTracker.Visualize(path);
        }
    }
}
