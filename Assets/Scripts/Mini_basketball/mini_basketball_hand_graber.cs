using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus; 

public class mini_basketball_hand_graber : OVRGrabber
{
    OVRHand hand;
    public float pinchThreshold = 0.1f;


    Vector3? m_myLastVelocity;
    Quaternion? m_myLastRotVelocity;

    Vector3 m_myLastPos;
    Quaternion m_myLastRot;

    protected override void Start()
    {
        base.Start();
        hand = GetComponent<OVRHand>();
    }


    public override void Update()
    {
        base.Update();
        CheckIndexPinch();


        if (!m_grabbedObj)
            return;

        var newVel =  (transform.position - m_myLastPos) / Time.deltaTime;
        var newRotVel = Quaternion.Lerp(transform.rotation * Quaternion.Inverse(m_myLastRot), Quaternion.identity, 1f / Time.deltaTime);


        if (m_myLastVelocity.HasValue)
            m_myLastVelocity = newVel.magnitude > m_myLastVelocity.Value.magnitude ? newVel : Vector3.Lerp(m_myLastVelocity.Value, newVel, 0.1f);
        else
            m_myLastVelocity = newVel;

        if (m_myLastRotVelocity.HasValue)
            m_myLastRotVelocity = Quaternion.Angle(newRotVel, Quaternion.identity) > Quaternion.Angle(m_myLastRotVelocity.Value, Quaternion.identity) ? newRotVel : Quaternion.Lerp(m_myLastRotVelocity.Value, newRotVel, 0.1f);
        else
            m_myLastRotVelocity =   newRotVel;

        m_myLastPos = transform.position;
        m_myLastRot = transform.rotation;
    }

    void CheckIndexPinch()
    {
        float pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        bool isPinching = pinchStrength > pinchThreshold;
        

        if (m_grabbedObj && (!isPinching || !GetComponent<OVRHand>().IsDataHighConfidence))
            GrabEnd();
        if (isPinching && m_grabCandidates.Count > 0)
            GrabBegin();
    }

    protected override void GrabEnd()
    {       
        if (m_grabbedObj) {
            Vector3 linearVelocity = m_myLastVelocity.Value;
            Vector3 angularVelocity = m_myLastRotVelocity.Value.eulerAngles;

            GrabbableRelease(linearVelocity, angularVelocity);

            Debug.Log("GrabbableRelease speed : " + m_lastPos + ", angular : " + angularVelocity);

            m_myLastVelocity = null;
            m_myLastRotVelocity = null;
        }

        GrabVolumeEnable(true);
    }
}
