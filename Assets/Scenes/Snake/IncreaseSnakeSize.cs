using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSnakeSize : MonoBehaviour
{
    [SerializeField] private int snakeSize;
    [SerializeField] private List<GameObject> body;
    [SerializeField] private GameObject head;
    [SerializeField] private Transform parent;

    void Start()
    {
        
    }

    void Update()
    {
        SnakeMove.dir dir = head.GetComponent<SnakeMove>().GetDir();

        if (body.Count < snakeSize) {
            Debug.Log("add elem");
            GameObject tmp = body[body.Count - 1];
            switch (dir) {
                case SnakeMove.dir.DOWN:
                    body.Add(Instantiate<GameObject>(body[0],
                        new Vector3(tmp.transform.position.x,
                                    tmp.transform.position.y + (0.02f + tmp.GetComponent<Collider>().bounds.size.y),
                                    tmp.transform.position.z),
                        Quaternion.identity, parent));
                    break;
                case SnakeMove.dir.UP:
                    body.Add(Instantiate<GameObject>(body[0],
                        new Vector3(tmp.transform.position.x,
                                    tmp.transform.position.y - (0.02f + tmp.GetComponent<Collider>().bounds.size.y),
                                    tmp.transform.position.z),
                        Quaternion.identity, parent));
                    break;
                case SnakeMove.dir.LEFT:
                    body.Add(Instantiate<GameObject>(body[0],
                        new Vector3(tmp.transform.position.x + (0.02f + tmp.GetComponent<Collider>().bounds.size.x),
                                    tmp.transform.position.y,
                                    tmp.transform.position.z),
                        Quaternion.identity, parent));
                    break;
                case SnakeMove.dir.RIGHT:
                    body.Add(Instantiate<GameObject>(body[0],
                        new Vector3(tmp.transform.position.x - (0.02f + tmp.GetComponent<Collider>().bounds.size.x),
                                    tmp.transform.position.y,
                                    tmp.transform.position.z),
                        Quaternion.identity, parent));
                    break;
            }

        }
    }
}
