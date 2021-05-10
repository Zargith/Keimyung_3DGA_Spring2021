using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreManager scoreManager { get; set; }

    private GameState gameState { get; set; }

    private void Start()
    {
        scoreManager = gameObject.AddComponent<ScoreManager>();
        gameState = gameObject.AddComponent<GameState>();
    }
}
