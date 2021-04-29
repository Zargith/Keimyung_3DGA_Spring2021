using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    internal virtual void OnHit()
    {
        Destroy(this.gameObject);
        // TODO: play chicken sound
    }
}
