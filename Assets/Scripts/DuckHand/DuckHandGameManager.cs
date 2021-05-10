using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class DuckHandGameManager : MonoBehaviour
{
    public Canvas PreGameCanvas;
    public Canvas InGameCanvas;

    public GameObject EnemyPrefab;


    private bool m_isRunning = false;
    private Stopwatch m_gameTime = new Stopwatch();

    public System.TimeSpan GameTime { get => m_gameTime.Elapsed; }

    public void StartGame()
    {
        PreGameCanvas.gameObject.SetActive(false);
        InGameCanvas.gameObject.SetActive(true);

        m_isRunning = true;

        m_gameTime.Restart();
    }


    public void Update()
    {
        if (!m_isRunning)
            return;

        if (Random.Range(0f, 1f) < Time.deltaTime)
        {
            var duck = Instantiate(EnemyPrefab, new Vector3(Random.Range(-3f, 3f), Random.Range(1f, 3f), Random.Range(-3f, 3f)), Quaternion.identity);

            duck.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);
        }
    }
}
