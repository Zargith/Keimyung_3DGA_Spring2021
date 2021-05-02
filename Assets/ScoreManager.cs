using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public int score;

    public void addToScore(int val)
    {
        score += val;
        print(score);
    }
}
