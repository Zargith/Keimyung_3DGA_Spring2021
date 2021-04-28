using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mini_basketball_score_manager : MonoBehaviour
{
    public Text scoreText;
    public mini_basketball_GameManager manager;
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "basketball_ball_valid") {
            other.gameObject.tag = "basketball_ball_invalid";
            if (manager.gameStarted) {
                manager.score++;
                scoreText.text = "Score: " + manager.score.ToString();
            }
            audioSource.Play();
        }
    }

    public void resetText()
    {
        scoreText.text = "Score: 0";
    }
}
