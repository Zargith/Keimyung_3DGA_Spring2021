using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckHandTimerDisplayer : MonoBehaviour
{
    private Text m_text;

    void Start()
    {
        m_text = GetComponent<Text>();
    }

    void Update()
    {
        m_text.text = FindObjectOfType<DuckHandGameManager>().GameTime.ToString("mm':'ss");
    }
}
