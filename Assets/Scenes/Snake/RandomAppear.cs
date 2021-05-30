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
        Appear(0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "snake") {
            Appear(Random.Range(0, 10) % 2);
            snake.GetComponent<SnakeMovement>().AddBodyPart();
            snake.GetComponent<SnakeMovement>().IncreaseSpeed();
        }
    }

    // Update is called once per frame
    public void Appear(int rand)
    {
        Vector3 pos = new Vector3(rand == 0 ? -0.132f : -0.095f, Random.Range(Down.transform.position.y + 0.02f, Up.transform.position.y - 0.02f), Random.Range(Right.transform.position.z + 0.02f, Left.transform.position.z - 0.02f));
        apple.transform.position = pos;
    }
}
