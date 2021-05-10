using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class DuckHandGameManager : MonoBehaviour
{
    [SerializeField] private Canvas PreGameCanvas;
    [SerializeField] private Canvas InGameCanvas;

    [SerializeField] private LevelScroller m_levelScroller;

    [SerializeField] private GameObject EnemyPrefab;

    [SerializeField] private int BaseHealth = 3;

    public System.TimeSpan GameTime { get => m_gameTime.Elapsed; }
    public int CurrentHealth { get; private set; }
    public int CurrentScore { get; private set; }

    private bool m_isRunning = false;
    private readonly Stopwatch m_gameTime = new Stopwatch();


    public void LoseHealth()
    {
        CurrentHealth--;

        if (CurrentHealth <= 0)
            EndGame();
    }
    public void OnEnemyKill()
    {
        CurrentScore++;
    }

    public void StartGame()
    {
        PreGameCanvas.gameObject.SetActive(false);
        InGameCanvas.gameObject.SetActive(true);

        m_isRunning = true;
        m_levelScroller.StartScrolling();
        CurrentHealth = BaseHealth;
        CurrentScore = 0;

        m_gameTime.Restart();
    }

    public void EndGame()
    {
        PreGameCanvas.gameObject.SetActive(true);

        m_isRunning = false;
        m_levelScroller.StopScrolling();
        m_gameTime.Stop();

        foreach (Transform child in m_levelScroller.transform)
            Destroy(child.gameObject);
    }

    private void Awake()
    {
        m_levelScroller = FindObjectOfType<LevelScroller>();
    }

    public void Update()
    {
        if (!m_isRunning)
            return;

        if (Random.Range(0f, 5f) < Time.deltaTime)
        {
            var duck = Instantiate(EnemyPrefab, new Vector3(Random.Range(-3f, 3f), Random.Range(1f, 3f), Random.Range(5f, 10f)), Quaternion.identity, m_levelScroller.transform);

            duck.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            //duck.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform);
        }
    }
}
