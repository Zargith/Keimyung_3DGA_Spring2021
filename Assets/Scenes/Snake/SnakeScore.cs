using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeScore : MonoBehaviour
{
	[SerializeField] Text scoreTxt;

	private int score = 0;

	public void UpdateScore()
	{
		int size = GetComponent<SnakeMovement>().Body.Count;
		if (size != score) {
			score = size;
			scoreTxt.text = "Score : " + ((size - 1) * 100).ToString();
		}
	}
}
