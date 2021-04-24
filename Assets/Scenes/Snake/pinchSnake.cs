using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class pinchSnake : MonoBehaviour
{
    OVRHand hand;
    public float pinchThreshold = 0.1f;

    void Start()
    {
        hand = GetComponent<OVRHand>();
    }

    public bool isPinching()
    {
        return (hand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > pinchThreshold);
    }

}
