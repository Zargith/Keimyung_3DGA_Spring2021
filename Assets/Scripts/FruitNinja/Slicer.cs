using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Launchable lch = other.GetComponentInParent<Launchable>();
        if (lch) {
            lch.Cut();
            lch.yolo();
        }
    }

}
