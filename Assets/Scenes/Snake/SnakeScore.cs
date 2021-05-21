using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeScore : MonoBehaviour
{
    [SerializeField] Text scoreTxt;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int size = GetComponent<SnakeMovement>().Body.Count;
        if (size != score) {
            score = size;
            scoreTxt.text = "Score : " + (size * 100).ToString();
        }   
    }
}
