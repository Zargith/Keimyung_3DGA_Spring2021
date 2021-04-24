using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini_basketball_reset_ball_tag : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "basketball_ball_invalid")
            other.gameObject.tag = "basketball_ball_valid";
    }
}
