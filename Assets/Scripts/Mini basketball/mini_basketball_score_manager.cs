using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mini_basketball_score_manager : MonoBehaviour
{
    public Text timerText;
    public int points = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "basketball_ball_valid") {
            other.gameObject.tag = "basketball_ball_invalid";
            points++;
            timerText.text = "Score: " + points.ToString();
        }
    }
}
