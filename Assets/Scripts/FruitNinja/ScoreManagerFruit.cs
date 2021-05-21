using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagerFruit : MonoBehaviour
{

    public int score;

    public void addToScore(int val)
    {
        score += val;
        print(score);
    }

    public void reset()
    {
        score = 0;
    }
}
