using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> BodyParts = new List<Transform>();

    public float minDistance = 0.25f;
    public float speed = 1f;
    public float rotationSpeed = 50f;
    public int size = 1;

    public GameObject bodyPrefab;

    private float dis;
    private Transform curBodyPart;
    private Transform prevBodyPart;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0 ; i < size - 1 ; ++i) {
            AddBodyPart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKey(KeyCode.E))
            AddBodyPart();
    }

    public void Move()
    {

        BodyParts[0].Translate(BodyParts[0].up
                * speed * Time.smoothDeltaTime, Space.World);
        if (Input.GetAxis("Horizontal") != 0) {
            // Change dir
            BodyParts[0].Rotate(Vector3.left *
                rotationSpeed *
                Time.deltaTime *
                Input.GetAxis("Horizontal"));
        }

        for (int i = 1 ; i < BodyParts.Count ; ++i) {
            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];

            dis = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

            Vector3 newPos = prevBodyPart.position;

            newPos.x = prevBodyPart.position.x;

            float T = Time.deltaTime * dis / minDistance * speed;

            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newPos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart()
    {
        Transform newPart = (Instantiate(bodyPrefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;

        newPart.SetParent(transform);
        BodyParts.Add(newPart);
    }

}
