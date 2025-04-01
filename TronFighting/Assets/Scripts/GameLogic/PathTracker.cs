using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathTracker : MonoBehaviour, ITrackable
{
    [SerializeField] private LineRenderer _lineRenderer;
    private List<Vector3> points;
    private Vector3 currentPoint;
    private Vector3 currentDirection;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public Vector3[] CalculatePath(Vector3 startPoint, Vector3 direction, int reflections, float maxDistance)
    {
        points = new List<Vector3>();
        currentPoint = startPoint;
        currentDirection = direction.normalized;

        points.Add(currentPoint);

        for (int i = 0; i < reflections; i++)
        {
            Ray ray = new Ray(currentPoint, currentDirection);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                currentPoint = hit.point;
                points.Add(currentPoint);
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);
            }
            else
            {
                points.Add(currentPoint + currentDirection * maxDistance);
                break;
            }
        }

        return points.ToArray();
    }

    public void Visualize(Vector3[] points)
    {
        _lineRenderer.positionCount = points.Length;
        _lineRenderer.SetPositions(points);
    }

    public void Clear()
    {
        _lineRenderer.positionCount = 0;
    }
}
