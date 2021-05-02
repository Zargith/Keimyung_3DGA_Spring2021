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

    public void SetMaxScore(int score)
    {
        maxScore = score;
    }

    public void ResetScore()
    {
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
                GameObject.Find("WinnerPanel").GetComponent<GameState>().NextRound(Player.Which.PLAYER);
                if (playerScore == maxScore)
                {
                    GameObject.Find("WinnerPanel").GetComponent<GameState>().EndGame(Player.Which.PLAYER);
                }
                break;
            case Player.Which.AI:
                aiScore++;
                UpdateScoreText(aiScoreText, aiScorePrefix, aiScore);
                GameObject.Find("WinnerPanel").GetComponent<GameState>().NextRound(Player.Which.AI);
                if (aiScore == maxScore)
                {
                    GameObject.Find("WinnerPanel").GetComponent<GameState>().EndGame(Player.Which.AI);
                }
                break;
        }
    }

    private void UpdateScoreText(Text text, string prefix, int score)
    {
        text.text = prefix + score;
    }
}
