using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class DuckHandGameManager : MonoBehaviour
{
    [SerializeField] private Canvas PreGameCanvas;
    [SerializeField] private Canvas InGameCanvas;

    [SerializeField] private DuckSpawner DuckSpawner;

    [SerializeField] private int BaseHealth = 3;

    public System.TimeSpan GameTime { get => m_gameTime.Elapsed; }
    public int CurrentHealth { get; private set; }
    public int CurrentScore { get; private set; }

    private bool m_isRunning = false;
    private readonly Stopwatch m_gameTime = new Stopwatch();


    [SerializeField] Vector3 InitialGravity = new Vector3(0, -3, 0);
    [SerializeField] Vector3 GravityChange = new Vector3(0, -0.1f, 0);
    private Vector3 GravityBackup;
    private void Start()
    {
        GravityBackup = Physics.gravity;
        Physics.gravity = InitialGravity;
    }
    private void OnDestroy()
    {
        Physics.gravity = GravityBackup;
    }

    private void Update()
    {
        Physics.gravity += GravityChange * Time.deltaTime;
    }

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

        DuckSpawner.StartSpawning();

        m_isRunning = true;
        CurrentHealth = BaseHealth;
        CurrentScore = 0;

        m_gameTime.Restart();
    }

    public void EndGame()
    {
        PreGameCanvas.gameObject.SetActive(true);

        DuckSpawner.StopSpawning();
        DuckSpawner.ClearAllDucks();

        m_isRunning = false;
        m_gameTime.Stop();
    }
    
}
