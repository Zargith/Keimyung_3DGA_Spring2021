using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text timerText;
    public float seconds = 60;
    public float miliseconds = 0;

    void Update()
    {
       /* if (!manager.gameStarted)
            return;

        if (miliseconds <= 0 && seconds <= 0)
        {
            timerText.text = "0:00";
            manager.gameStarted = false;
            return;
        }*/

        if (miliseconds <= 0)
        {
            if (seconds >= 0)
                seconds--;
            miliseconds = 100;
        }
        miliseconds -= Time.deltaTime * 100;
        timerText.text = seconds.ToString() + ":" + ((int)miliseconds).ToString();
    }
}
