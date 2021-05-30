using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGameManager : MonoBehaviour
{

    ScoreManagerFruit smf;

    [SerializeField] GameObject button;
    button_animation ba;

    [SerializeField] GameObject spawnerPrefab;
    GameObject spawnerInstance;
    Spawner spScript;


    bool gameStarted = false;

    void Start()
    {
        smf = GetComponent<ScoreManagerFruit>();
        ba = button.GetComponentInChildren<button_animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ba.isButtonPushed() && !gameStarted)
        {
            print("aaa");
            spawnerInstance = Instantiate(spawnerPrefab);
            gameStarted = true;
            spScript = spawnerInstance.GetComponent<Spawner>();
            smf.reset();
        }
        if (gameStarted)
        {
            if (!ba.isButtonPushed())
            {
                button.SetActive(false);
            }
            if (spScript._stop)
            {
                Destroy(spawnerInstance);
                button.SetActive(true);
                gameStarted = false;
            }
        }
    }
}
