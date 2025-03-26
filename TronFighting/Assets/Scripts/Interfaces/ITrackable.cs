using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrackable
{
    Vector3[] CalculatePath(Vector3 startPoint, Vector3 direction, int reflections, float maxDistance);
}
