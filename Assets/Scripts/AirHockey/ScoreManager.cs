using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour

{
    private const string playerScorePrefix = "Player Score: ";

    private const string aiScorePrefix = "AI Score: ";

    public Text playerScoreText;

    public Text aiScoreText;

    private int maxScore = 3;

    private int playerScore = 0;

    private int aiScore = 0;
    public bool finished { get; private set; } = false;

    public Player.Which winner { get; private set; } = Player.Which.NONE;

    public void SetMaxScore(int score)
    {
        maxScore = score;
    }

    public void ResetScore()
    {
        finished = false;
        winner = Player.Which.NONE;
        playerScore = 0;
        aiScore = 0;
        UpdateScoreText(playerScoreText, playerScorePrefix, playerScore);
        UpdateScoreText(aiScoreText, aiScorePrefix, aiScore);
    }

    public void AddScore(Player.Which player)
    {
        switch (player)
        {
            case Player.Which.PLAYER:
                playerScore++;
                UpdateScoreText(playerScoreText, playerScorePrefix, playerScore);
                GameObject.Find("GameStatePanel").GetComponent<GameState>().NextRound(Player.Which.PLAYER);
                if (playerScore == maxScore)
                {
                    finished = true;
                    winner = Player.Which.PLAYER;
                    //GameObject.Find("WinnerPanel").GetComponent<GameState>().EndGame(Player.Which.PLAYER);
                }
                break;
            case Player.Which.AI:
                aiScore++;
                UpdateScoreText(aiScoreText, aiScorePrefix, aiScore);
                GameObject.Find("GameStatePanel").GetComponent<GameState>().NextRound(Player.Which.AI);
                if (aiScore == maxScore)
                {
                    finished = true;
                    winner = Player.Which.AI;
                    //GameObject.Find("WinnerPanel").GetComponent<GameState>().EndGame(Player.Which.AI);
                }
                break;
        }
    }

    public Player.Which getMostPointPlayer()
    {
        if (playerScore > aiScore)
            return Player.Which.PLAYER;
        else if (aiScore > playerScore)
            return Player.Which.AI;
        return Player.Which.NONE;
    }

    private void UpdateScoreText(Text text, string prefix, int score)
    {
        text.text = prefix + score;
    }
}
