using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Gesture
{
    public string name;

    // Local rotation of each bone
    public List<Quaternion> bones;
    public Hand hand;
}
