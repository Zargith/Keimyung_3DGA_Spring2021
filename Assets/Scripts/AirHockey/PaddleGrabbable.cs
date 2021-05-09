using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaddleGrabbable : MonoBehaviour
{
    [SerializeField] private GestureProcessor GestureProcessor;
    [SerializeField] private OVRSkeleton LeftHand;
    [SerializeField] private OVRSkeleton RightHand;
    [SerializeField] private float MaxDistance = 0.1f;
    [SerializeField] private float DetectionThreshold = 0.1f;

    private bool IsGrabbed => m_currentHolder != null;

    private Hand? m_currentHoldingHand;
    private OVRSkeleton m_currentHolder;

    void Update()
    {
        if (IsGrabbed)
        {
            ManageMovement();
            CheckRelease();
        }
        else
        {
            CheckGrab(Hand.Left, LeftHand);
            CheckGrab(Hand.Right, RightHand);
        }
    }

    private void ManageMovement()
    {
        var targetPos = GetHandPosition(m_currentHolder);

        var delta = targetPos - transform.position;
        delta.y = 0;

        GetComponent<Rigidbody>().velocity = delta / Time.deltaTime;
    }

    private void CheckGrab(Hand hand, OVRSkeleton handSkeleton)
    {
        if (IsGrabbing(hand, handSkeleton.transform) && Vector3.Distance(transform.position, handSkeleton.transform.position) < MaxDistance)
        {
            m_currentHoldingHand = hand;
            m_currentHolder = handSkeleton;
        }
    }

    private void CheckRelease()
    {
        if (!IsGrabbing(m_currentHoldingHand.Value, m_currentHolder.transform))
        {
            m_currentHoldingHand = null;
            m_currentHolder = null;
        }
    }


    private bool IsGrabbing(Hand hand, Transform handTransform)
    {
        return GestureProcessor.CompareGesture(hand, "PaddleGrab") > DetectionThreshold;
    }

    private Vector3 GetHandPosition(OVRSkeleton skeleton)
    {
        List<Vector3> fingertipPos = new List<Vector3>
        {
            skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_ThumbTip].Transform.position,
            skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position,
            skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_MiddleTip].Transform.position,
            skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_RingTip].Transform.position,
            skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_PinkyTip].Transform.position
        };

        return fingertipPos.Aggregate((a, b) => a + b) / fingertipPos.Count;
    }
}
