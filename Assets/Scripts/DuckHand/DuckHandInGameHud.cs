using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckHandInGameHud : MonoBehaviour
{
    [SerializeField] private Text m_timeDisplayText;
    [SerializeField] private Text m_scoreDisplayText;
    [SerializeField] private Text m_healthDisplayText;

    private DuckHandGameManager m_gameManager;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<DuckHandGameManager>();
    }

    void Update()
    {
        m_timeDisplayText.text = m_gameManager.GameTime.ToString("mm':'ss");
        m_scoreDisplayText.text = $"Score : {m_gameManager.CurrentScore}";
        m_healthDisplayText.text = $"Health : {m_gameManager.CurrentHealth}";
    }
}
