using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRayMatcher : MonoBehaviour
{
    public void PositionToMatchRay(Ray r, Vector3 up)
    {
        transform.position = r.origin;
        transform.rotation = Quaternion.LookRotation(r.direction, up);
    }
}
