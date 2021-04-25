using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GestureProcessor : MonoBehaviour
{
    public OVRSkeleton LeftHand;
    public OVRSkeleton RightHand;

    /// <summary>
    /// Read current gesture from given hand
    /// </summary>
    /// <returns>Gesture OR NULL IF GIVEN HAND HAS NOT YET BEEN INSTANCIATED (aka. the sensor did not yeat provide any data about this hand)  </returns>
    public Gesture? ReadGesture(Hand hand)
    {
        var skeleton = (hand == Hand.Left ? LeftHand : RightHand);

        if (skeleton.Bones.Count == 0)
            return null;

        return new Gesture
        {
            bones = skeleton.Bones
            .Select(b => b.Transform.localRotation).ToList(),
            hand = hand
        };
    }

    // TODO: Handle gesture mirroring depending on the hand

    /// <returns>[0, 1] How similar are the two gestures </returns>
    public float CompareGesture(Hand hand, string gesturename)
    {
        return CompareGesture(hand, GetComponent<GestureDb>().GetGesture(gesturename));
    }

    public float CompareGesture(Hand hand, Gesture gesture)
    {
        var targetHandGesture = ReadGesture(hand);

        if (targetHandGesture == null)
            return 0;

        //if (targetHandGesture.Value.hand != gesture.hand) gesture = gesture.mirror(); or something

        return targetHandGesture.Value.bones.Zip(gesture.bones,
            (expected, actual) => GetBoneCorrespondance(expected, actual)
        ).Aggregate((a, b) => a * b) ;
    }

    private float GetBoneCorrespondance(Quaternion a, Quaternion b)
    {
        var maxAngle = Quaternion.Angle(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 180, 0));
        var angle = Quaternion.Angle(a, b);

        return 1 - (angle / maxAngle);
    }
}
