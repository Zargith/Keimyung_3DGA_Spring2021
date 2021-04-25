using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHandGameManager : MonoBehaviour
{
    public Canvas PreGameCanvas;

    public GameObject EnemyPrefab;


    private bool m_isRunning = false;

    public void StartGame()
    {
        PreGameCanvas.enabled = false;

        m_isRunning = true;
    }


    public void Update()
    {
        if (!m_isRunning)
            return;

        if (Random.Range(0f, 1f) < Time.deltaTime)
            Instantiate(EnemyPrefab, new Vector3(Random.Range(-3f, 3f), Random.Range(1f, 3f), Random.Range(-3f, 3f)), Quaternion.identity);
    }
}
