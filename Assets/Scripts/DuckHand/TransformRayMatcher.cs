using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRayMatcher : MonoBehaviour
{
    public Transform AnchorRay;

    public void PositionToMatchRay(Ray r, Vector3 up)
    {
        // Does not work, why ?! even rotation only is not even close

        //transform.rotation = AnchorRay.rotation * Quaternion.Inverse(Quaternion.LookRotation(r.direction, up));
        //transform.position = AnchorRay.position - r.origin;
    }
}
