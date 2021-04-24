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
    private bool _change = false;

    public enum plane
    {
        FRONT,
        LEFT,
        BACK,
        RIGHT
    }

    private enum Dir
    {
        RIGHT,
        LEFT,
        NONE
    }

    private Dir dir = Dir.LEFT;
    private Dir preDir = Dir.LEFT;

    private float time = 0f;

    public plane _currPlane = plane.FRONT;
    private plane _pastPlane = plane.RIGHT;
    private Vector3 prevPos;
    private Vector3 prevRot;

    // Start is called before the first frame update
    void Start()
    {
        prevPos = BodyParts[0].transform.position;
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

        plane tmp = BodyParts[0].GetComponent<ChangePanel>().GetBodyDir();
        if (tmp != _currPlane) {
            _pastPlane = _currPlane;
            _currPlane = tmp;

            Debug.Log("past : " + _pastPlane);
            Debug.Log("curr : " + _currPlane);
        }

        // Move
        if ((_currPlane == plane.FRONT && _pastPlane == plane.RIGHT) ||
            (_currPlane == plane.LEFT && _pastPlane == plane.FRONT) ||
            (_currPlane == plane.BACK && _pastPlane == plane.LEFT) ||
            (_currPlane == plane.RIGHT && _pastPlane == plane.BACK)) {
            BodyParts[0].Translate((_currPlane == plane.RIGHT ? BodyParts[0].right :
                _currPlane == plane.LEFT ? BodyParts[0].right * -1 :
                _currPlane == plane.FRONT ? BodyParts[0].forward : BodyParts[0].forward * -1)
                * speed * Time.smoothDeltaTime, Space.World);
        } else if ((_currPlane == plane.RIGHT && _pastPlane == plane.FRONT) ||
            (_currPlane == plane.BACK && _pastPlane == plane.RIGHT) ||
            (_currPlane == plane.LEFT && _pastPlane == plane.BACK) ||
            (_currPlane == plane.FRONT && _pastPlane == plane.LEFT)) {
            BodyParts[0].Translate((_currPlane == plane.RIGHT ? BodyParts[0].right * -1 :
                _currPlane == plane.LEFT ? BodyParts[0].right :
                _currPlane == plane.FRONT ? BodyParts[0].forward * -1 : BodyParts[0].forward)
                * speed * Time.smoothDeltaTime, Space.World);
        }

        if (Input.GetAxis("Horizontal") != 0) {
            // Change dir
            preDir = Input.GetAxis("Horizontal") > 0 ? Dir.RIGHT : Dir.LEFT;
            BodyParts[0].Rotate((_currPlane == plane.RIGHT ? Vector3.forward : _currPlane == plane.LEFT ? Vector3.forward : Vector3.right) *
                rotationSpeed *
                Time.deltaTime *
                (_currPlane == plane.RIGHT ? -1 : _currPlane == plane.LEFT ? 1 : -1) * Input.GetAxis("Horizontal"));
        }

        for (int i = 1 ; i < BodyParts.Count ; ++i) {
            curBodyPart = BodyParts[i];
            prevBodyPart = BodyParts[i - 1];

            dis = Vector3.Distance(prevBodyPart.position, curBodyPart.position);

            Vector3 newPos = prevBodyPart.position;

            if (curBodyPart.GetComponent<ChangePanel>().GetBodyDir() == plane.LEFT || curBodyPart.GetComponent<ChangePanel>().GetBodyDir() == plane.RIGHT)
                newPos.z = prevBodyPart.position.z;
            else
                newPos.x = prevBodyPart.position.x;

            float T = Time.deltaTime * dis / minDistance * speed;

            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newPos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, prevBodyPart.rotation, T);
        }
        prevPos = BodyParts[0].transform.position;

    }

    public void AddBodyPart()
    {
        Transform newPart = (Instantiate(bodyPrefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;

        newPart.SetParent(transform);
        BodyParts.Add(newPart);
    }
}
