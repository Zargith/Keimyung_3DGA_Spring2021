using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] GameObject[] buttons;

    public Text timerText;
    public float seconds = 3;

    public List<GameObject> Body = new List<GameObject>();
    private List<Transform> BodyParts = new List<Transform>();

    public float minDistance = 0.025f;
    public float speed = 1f;
    public float rotationSpeed = 50f;
    public int size = 1;

    [SerializeField] GameObject Apple;

    public GameObject bodyPrefab;

    private float dis;
    private Transform curBodyPart;
    private Transform prevBodyPart;
    private Vector3 initPos;
    bool firstPlane = true;
    private float originSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originSpeed = speed;
        BodyParts.Add(Body[0].transform);
        initPos = BodyParts[0].transform.position;
        for (int i = 0 ; i < size - 1 ; ++i) {
            AddBodyPart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds <= 0) {
            Move();
        } else
            DecreaseTime();

        if (Input.GetKey(KeyCode.E))
            AddBodyPart();
        if (Input.GetKeyDown(KeyCode.A) || buttons[1].GetComponent<button_animation>().isButtonPushed())
            ChangePlan();
    }

    void DecreaseTime()
    {
        seconds -= 1 * Time.deltaTime;
        timerText.text = ((int)seconds).ToString();
        if (seconds <= 0)
            timerText.text = "";
    }

    void ChangePlan()
    {
        if (firstPlane) {
            firstPlane = false;
            transform.position = new Vector3(-0.105f, transform.position.y, transform.position.z);
        } else {
            firstPlane = true;
            transform.position = new Vector3(-0.143f, transform.position.y, transform.position.z);
        }
    }

    public void IncreaseSpeed()
    {
        speed += 0.025f;
    }

    public void End()
    {
        seconds = 4;
        for (int i = 1 ; i < Body.Count ; ++i) {
            Destroy(Body[i]);
        }
        BodyParts.Clear();
        BodyParts.Add(Body[0].transform);
        BodyParts[0].position = initPos;
        Apple.GetComponent<RandomAppear>().Appear(0);
        speed = originSpeed;
        BodyParts[0].transform.rotation = new Quaternion(0, 0, 0, 0);
        firstPlane = true;
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
        } else if (buttons[0].GetComponent<button_animation>().isButtonPushed() ||
            buttons[2].GetComponent<button_animation>().isButtonPushed()) {
            BodyParts[0].Rotate(Vector3.left *
               rotationSpeed *
               Time.deltaTime *
               (buttons[0].GetComponent<button_animation>().isButtonPushed() ? -1 : 1));
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
        GameObject newPart = Instantiate(bodyPrefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation);
        Transform newElem = newPart.transform;

        newElem.SetParent(transform);
        BodyParts.Add(newElem);
        Body.Add(newPart);
    }
}
