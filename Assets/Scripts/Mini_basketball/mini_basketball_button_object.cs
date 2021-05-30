using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini_basketball_button_object : MonoBehaviour
{
	[SerializeField] mini_basketball_GameManager manager;
	[SerializeField] GameObject topPart;
	bool buttonIsPushed = false;
	double startPosition;
	double endPosition;
	[SerializeField] float movingSpeed = 0.25f;
	AudioSource audioSource;

	void Start()
	{
		startPosition = topPart.transform.position.y;
		endPosition = startPosition - 0.085;
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (buttonIsPushed)
			topPart.transform.Translate(Vector3.down * Time.deltaTime * movingSpeed);
		else if (!buttonIsPushed && topPart.transform.position.y < startPosition && !manager.gameStarted)
			topPart.transform.Translate(Vector3.up * Time.deltaTime * movingSpeed);

		if (topPart.transform.position.y <= endPosition)
			buttonIsPushed = false;
	}


	void OnMouseDown() {
		if (!manager.gameStarted) {
			buttonIsPushed = true;
			audioSource.Play();
			manager.startGame();
		}
	}

	 void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("player_hand") && !manager.gameStarted) {
			buttonIsPushed = true;
			audioSource.Play();
			manager.startGame();
		}
	
}

	public bool isButtonPushed()
	{
		return buttonIsPushed;
	}
}
