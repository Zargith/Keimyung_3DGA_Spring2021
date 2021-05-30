using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeWallCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other " + other.gameObject.tag);
        Debug.Log("object " + gameObject.tag);
        if ((other.gameObject.tag == "obstacle" && gameObject.tag != "snakeHead") || (gameObject.tag == "snakeHead" && other.gameObject.tag == "bodyPart"))
            GetComponentInParent<SnakeMovement>().End();
    }
}
