using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeScore : MonoBehaviour
{
	[SerializeField] Text scoreTxt;

	private int score = 0;

	public void ResetScore()
	{
		score = 0;
	}

	public int GetScore()
	{
		return score;
	}

	public void UpdateScore()
	{
		score += 100;
		scoreTxt.text = "Score : " + score.ToString();
	}
}
