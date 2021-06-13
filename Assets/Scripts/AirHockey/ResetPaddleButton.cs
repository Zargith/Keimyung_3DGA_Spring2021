using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPaddleButton : MonoBehaviour
{
    [SerializeField] GameObject playerPaddle;
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
        else if (!buttonIsPushed && topPart.transform.position.y < startPosition)
            topPart.transform.Translate(Vector3.up * Time.deltaTime * movingSpeed);

        if (topPart.transform.position.y <= endPosition)
            buttonIsPushed = false;
    }


    void OnMouseDown()
    {
        buttonIsPushed = true;
        audioSource.Play();

        ResetPlayerPaddle();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player_hand"))
        {
            buttonIsPushed = true;
            audioSource.Play();

            ResetPlayerPaddle();
        }

    }

    private void ResetPlayerPaddle()
    {
        playerPaddle.transform.position = new Vector3(-0.6f, 0.34f, -0.013f);
        playerPaddle.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerPaddle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public bool isButtonPushed()
    {
        return buttonIsPushed;
    }
}
