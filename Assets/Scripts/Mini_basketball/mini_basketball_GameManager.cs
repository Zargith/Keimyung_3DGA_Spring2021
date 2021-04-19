using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini_basketball_GameManager : MonoBehaviour
{
    public mini_basketball_timer timer;
    public mini_basketball_score_manager scoreManager;
    public GameObject prefabBallsSpawner;
    public GameObject prefabBall;
    private GameObject ballsSpawner = null;

    public bool gameStarted = false;
    public int score = 0;

    public void startGame()
    {
        if (ballsSpawner != null)
            Object.Destroy(ballsSpawner);
        gameStarted = true;
        score = 0;
        timer.seconds = 30;
        scoreManager.resetText();
        ballsSpawner = Instantiate(prefabBallsSpawner);
        instanciateNewBall(new Vector3(0f, 0.7f, 1.4f));
        instanciateNewBall(new Vector3(0.3f, 0.7f, 1.4f));
        instanciateNewBall(new Vector3(-0.3f, 0.7f, 1.4f));
    }

    private void instanciateNewBall(Vector3 position)
    {
        GameObject newBall = Instantiate(prefabBall, position, Quaternion.identity);
        newBall.tag = "basketball_ball_valid";
        newBall.transform.parent = ballsSpawner.transform;
    }
}