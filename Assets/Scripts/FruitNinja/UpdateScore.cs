using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] Text text;
    ScoreManagerFruit man;


    private void Start()
    {
        man = FindObjectOfType<ScoreManagerFruit>();
    }

    void Update()
    {
        text.text = man.score.ToString();
    }
}
