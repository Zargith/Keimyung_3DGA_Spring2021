using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    Text text;
    ScoreManagerFruit man;


    private void Start()
    {
        man = FindObjectOfType<ScoreManagerFruit>();
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = man.score.ToString();
    }
}
