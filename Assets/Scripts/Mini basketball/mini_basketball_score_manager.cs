using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini_basketball_score_manager : MonoBehaviour
{
    public int points = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "basketball_ball_valid") {
            points++;
            other.gameObject.tag = "basketball_ball_invalid";
        }
    }
}
