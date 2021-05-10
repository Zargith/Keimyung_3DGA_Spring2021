using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAppear : MonoBehaviour
{
    [SerializeField] private GameObject Up = null;
    [SerializeField] private GameObject Down = null;
    [SerializeField] private GameObject Left = null;
    [SerializeField] private GameObject Right = null;

    [SerializeField] GameObject apple;
    [SerializeField] GameObject snake;

    // Start is called before the first frame update
    void Start()
    {
        Appear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "snake") {
            Appear();
            snake.GetComponent<SnakeMovement>().AddBodyPart();
        }
    }

    // Update is called once per frame
    void Appear()
    {
        Vector3 pos = new Vector3(44.1f, Random.Range(Down.transform.position.y + 1, Up.transform.position.y - 1), Random.Range(Right.transform.position.z + 1, Left.transform.position.z - 1));
        apple.transform.position = pos;
    }
}
