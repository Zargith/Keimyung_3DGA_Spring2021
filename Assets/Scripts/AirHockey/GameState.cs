using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public Text winnerText;

    public void StartGame()
    {
        
    }

    public void PauseGame()
    {

    }

    public void NextRound(Player.Which winner)
    {
        switch (winner)
        {
            case Player.Which.PLAYER:
                GameObject.Find("Puck").GetComponent<Puck>().Stop();
                GameObject.Find("Puck").GetComponent<Transform>().position = new Vector3(-0.470999986F, 0.337000012F, -0.0280000009F);
                break;
            case Player.Which.AI:
                GameObject.Find("Puck").GetComponent<Puck>().Stop();
                GameObject.Find("Puck").GetComponent<Transform>().position = new Vector3(-0.470999986F, 0.337000012F, -0.0280000009F); // A modifier
                break;
        }
    }

    public void EndGame(Player.Which winner)
    {
        switch (winner)
        {
            case Player.Which.PLAYER:
                winnerText.text = "Player win !";
                break;
            case Player.Which.AI:
                winnerText.text = "AI win !";
                break;
        }
    }

    public void Quit()
    {

    }
}
