using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_animation : MonoBehaviour
{
	[SerializeField] GameObject topPart;
	bool buttonIsPushed = false;
	float startPosition;
	float endPosition;
	[SerializeField] float movingSpeed = 0.25f;
	[SerializeField] bool collideWithMouse = false;
	[SerializeField] bool collideWithTaggedGameObject = false;
	[SerializeField] string colliderTag = null;
	AudioSource audioSource;

	void Start()
	{
		startPosition = topPart.transform.position.y;
		endPosition = startPosition - 0.085f;
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (buttonIsPushed)
			topPart.transform.Translate(Vector3.down * Time.deltaTime * movingSpeed);
		else if (!buttonIsPushed && topPart.transform.position.y < startPosition)
			topPart.transform.Translate(Vector3.up * Time.deltaTime * movingSpeed);
		else if (!buttonIsPushed && topPart.transform.position.y > startPosition)
			topPart.transform.position = new Vector3(topPart.transform.position.x, startPosition, topPart.transform.position.z);

		if (topPart.transform.position.y <= endPosition)
			buttonIsPushed = false;
	}

	void OnMouseDown()
	{
		if (collideWithMouse) {
			buttonIsPushed = true;
			audioSource.Play();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (collideWithTaggedGameObject) {
			if (colliderTag == null || colliderTag.Trim() == "") {
				Debug.LogError("colliderTag needs to be not null or empty");
				return;
			} {
			if (other.gameObject.CompareTag(colliderTag))
				buttonIsPushed = true;
				audioSource.Play();
			}
		}
	}

	public bool isButtonPushed()
	{
		return buttonIsPushed;
	}
}
