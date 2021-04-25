using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini_basketball_button_object_spawn_ball_in_top_of_loop : MonoBehaviour
{
    public GameObject topPart;
    private bool buttonIsPushed = false;
    private double startPosition;
    private double endPosition;
    public float movingSpeed = 0.25f;
    public GameObject prefabBall;

    void Start()
    {
        startPosition = topPart.transform.position.y;
        endPosition = startPosition - 0.085;
    }

    void Update()
    {
        if (buttonIsPushed)
            topPart.transform.Translate(Vector3.down * Time.deltaTime * movingSpeed);
        else if (!buttonIsPushed && topPart.transform.position.y < startPosition)
            topPart.transform.Translate(Vector3.up * Time.deltaTime * movingSpeed);

        if (topPart.transform.position.y <= endPosition)
            buttonIsPushed = false;
    }
    void OnMouseDown() {
        buttonIsPushed = true;
        GameObject newBall = Instantiate(prefabBall, new Vector3((float)4.8e-11, 2, 2.7f), Quaternion.identity);
        newBall.tag = "basketball_ball_valid";
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player_hand") {
            buttonIsPushed = true;
            GameObject newBall = Instantiate(prefabBall, new Vector3((float)4.8e-11, 2, 2.7f), Quaternion.identity);
            newBall.tag = "basketball_ball_valid";
        }
    }
}
