using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GestureDb : MonoBehaviour
{
    public List<Gesture> GestureDatabase = new List<Gesture>();

    public OVRSkeleton skeleton;
    
    public bool CreationMode = true;
    public string NewGestureName;
    public KeyCode GestureCreationKey = KeyCode.Space;


    public Gesture GetGesture(string name)
    {
        try
        {
            return GestureDatabase.First(g => g.name == name);
        } catch (Exception)
        {
            throw new Exception("Unknown gesture name '" + name + "'");
        }
    }

    void Update()
    {
        if (CreationMode && Input.GetKeyDown(GestureCreationKey))
        {
            var hand = GetBoundHand();

            if (hand == null)
            {
                Debug.LogWarning("Cannot capture hand gesture : skeleton doesn't exist for this hand yet. (skeleton.GetSkeletonType() is `None`)");
                return;
            }

            var newGesture = FindObjectOfType<GestureProcessor>().ReadGesture(hand.Value);

            if (newGesture == null)
            {
                Debug.LogWarning("Cannot capture hand gesture : skeleton doesn't exist for this hand yet");
                return;
            }

            GestureDatabase.Add(new Gesture()
            {
                name = NewGestureName,
                bones = newGesture.Value.bones,
                hand = newGesture.Value.hand
            });
        }
    }

    private Hand? GetBoundHand()
    {
        switch (skeleton.GetSkeletonType())
        {
            case OVRSkeleton.SkeletonType.HandLeft:
                    return Hand.Left;
            case OVRSkeleton.SkeletonType.HandRight:
                return Hand.Right;
            default:
                return null;
        }
    }
}
