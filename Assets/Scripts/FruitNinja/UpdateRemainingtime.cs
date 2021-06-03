using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateRemainingtime : MonoBehaviour
{
    Text text;
    Spawner man;
    int maxTime;
    float t;


    private void Start()
    {
        man = FindObjectOfType<Spawner>();
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (!man)
        {
            man = FindObjectOfType<Spawner>();
            if (man) maxTime = man.getMaxTime();
            return;
        }
        if (man.isStarted() && !man._stop)
        {
            int r = maxTime - (int)t ;
            text.text = r.ToString() + " s";
            t += Time.deltaTime;
        }
    }
}
