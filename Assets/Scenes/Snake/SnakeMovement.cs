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
	[SerializeField] GameObject startButton;
	[SerializeField] GameObject highScoreTxt;

	public GameObject bodyPrefab;

	private float dis;
	private Transform curBodyPart;
	private Transform prevBodyPart;
	private Vector3 initPos;
	private Vector3 initPosElem;
	bool firstPlane = true;
	private float originSpeed;
	bool start = false;

	// Start is called before the first frame update
	void Start()
	{
		originSpeed = speed;
		initPosElem = transform.position;
		BodyParts.Add(Body[0].transform);
		initPos = BodyParts[0].transform.position;
		for (int i = 0 ; i < size - 1 ; ++i) {
			AddBodyPart();
		}
		startButton.GetComponent<Button>().onClick.AddListener(delegate {
			SetStart(true);
		});
	}

	public void SetStart(bool newStartValue)
    {
		highScoreTxt.SetActive(!newStartValue);
		startButton.SetActive(!newStartValue);
		start = newStartValue;
    }

	void SetHighScore(int score)
	{
		PlayerPrefs.SetInt("SnakeHighScore", score);
	}

	bool isChangingPlan = false;
	void Update()
	{
		if (seconds <= 0) {
			Move();
		} else if (start) {
			DecreaseTime();
        }

		if (Input.GetKey(KeyCode.E))
			AddBodyPart();

		bool pushedButton1 = buttons[1].GetComponent<button_animation>().isButtonPushed();
		if (!pushedButton1 && isChangingPlan)
			isChangingPlan = false;

		if ((Input.GetKeyDown(KeyCode.A) || pushedButton1) && !isChangingPlan)
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
		isChangingPlan = true;
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
		speed += 0.01f;
	}

	public void End()
	{
		seconds = 4;
		int score = GetComponent<SnakeScore>().GetScore();
		int highScore = PlayerPrefs.GetInt("SnakeHighScore");
		if (score > highScore) {
			highScore = score;
			SetHighScore(score);
		}
		highScoreTxt.GetComponent<Text>().text = "HighScore: " + highScore.ToString();
		GameObject tmp = Body[0];
		for (int i = 1 ; i < Body.Count ; ++i) {
			Destroy(Body[i]);
		}
		Body.Clear();
		Body.Add(tmp);
		BodyParts.Clear();
		BodyParts.Add(Body[0].transform);
		transform.position = initPosElem;
		BodyParts[0].position = initPos;
		Apple.GetComponent<RandomAppear>().Appear(0);
		speed = originSpeed;
		BodyParts[0].transform.rotation = new Quaternion(0, 0, 0, 0);
		firstPlane = true;
		isRotating = false;
		isChangingPlan = false;
		SetStart(false);
		GetComponent<SnakeScore>().ResetScore();
	}

	bool isRotating = false;

	public void Move()
	{
		bool pushedButton0 = buttons[0].GetComponent<button_animation>().isButtonPushed();
		bool pushedButton2 = buttons[2].GetComponent<button_animation>().isButtonPushed();

		if (!pushedButton0 && !pushedButton2 && isRotating)
			isRotating = false;

		if ((pushedButton0 || pushedButton2) && !isRotating) {
			isRotating = true;
			BodyParts[0].Rotate(Vector3.left * (pushedButton0 ? -1 : 1) * 90);
		}
		BodyParts[0].Translate(BodyParts[0].up * speed * Time.smoothDeltaTime, Space.World);
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
		GetComponent<SnakeScore>().UpdateScore();
		StartCoroutine("AddTag", newPart);
	}

	IEnumerator AddTag(GameObject a)
    {
		yield return new WaitForSeconds(2f);
		if (a)
			a.tag = "bodyPart";
    }
}
