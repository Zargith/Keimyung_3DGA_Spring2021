using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus; 

public class mini_basketball_hand_graber : OVRGrabber
{
    OVRHand hand;
    public float pinchThreshold = 0.7f;

    protected override void Start()
    {
        base.Start();
        hand = GetComponent<OVRHand>();
    }


    public override void Update()
    {
        base.Update();
        CheckIndexPinch();
    }

    void CheckIndexPinch()
    {
        float pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        bool isPinching = pinchStrength > pinchThreshold;

        if (m_grabbedObj && isPinching && m_grabCandidates.Count > 0)
            GrabBegin();
        else
            GrabEnd();
    }

    protected override void GrabEnd()
    {
        if (m_grabbedObj) {
            Vector3 linearVelocity = (transform.position - m_lastPos) / Time.fixedDeltaTime;
            Vector3 angularVelocity = (transform.eulerAngles - m_lastRot.eulerAngles) / Time.fixedDeltaTime;

            GrabbableRelease(linearVelocity, angularVelocity);
        }

        GrabVolumeEnable(true);
    }
}
