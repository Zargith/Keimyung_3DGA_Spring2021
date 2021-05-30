using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text timerText;

    private int minutes = 1;

    private int seconds = 30;

    private float miliseconds = 0;

    public bool started { get; private set; } = false;

    public bool finished { get; private set; } = false;

    void Update()
    {
        if (started && !finished)
        {
            if ((minutes <= 0 && seconds <= 0 && miliseconds <= 0) || minutes < 0) //temp
            {
                finished = true;
            }
            if (miliseconds <= 0)
            {
                if (seconds >= 0)
                    seconds--;
                miliseconds = 100;
            }
            if (seconds <= 0)
            {
                if (minutes >= 0)
                    minutes--;
                seconds = 60;
            }
            miliseconds -= Time.deltaTime * 100;
            timerText.text = minutes.ToString() + ":" + seconds.ToString();
        }
    }

    public void Launch()
    {
        started = true;
    }

    public void Restart()
    {
        minutes = 1;
        seconds = 30;
        started = true;
        finished = false;
    }
}
