using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private ScoreManager scoreManager;

    private GameState gameState;

    private TimerManager timerManager;

    private GameObject endGameMenu;

    private void Start()
    {
        scoreManager = GameObject.Find("ScorePanel").GetComponent<ScoreManager>();
        gameState = GameObject.Find("GameStatePanel").GetComponent<GameState>();
        timerManager = GameObject.Find("TimerPanel").GetComponent<TimerManager>();
        endGameMenu = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g => g.name == "EndGameMenu");
        timerManager.Launch();
    }

    private void Update()
    {
        if (timerManager.finished)
        {
            endGameMenu.SetActive(true);
            //timerManager;
        }
        if (scoreManager.finished)
        {
            endGameMenu.SetActive(true);
        }
    }
}
