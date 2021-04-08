using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_animation : MonoBehaviour
{
    public GameObject topPart;
    private bool buttonIsPushed = false;
    private double startPosition;
    private double endPosition;
    public float movingSpeed = 0.25f;
    public bool collideWithMouse = false;
    public bool collideWithTaggedGameObject = false;
    public string colliderTag = null;

    void Start() {
        startPosition = topPart.transform.position.y;
        endPosition = startPosition - 0.085;
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

    void OnMouseDown() {
        if (collideWithMouse)
            buttonIsPushed = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (collideWithTaggedGameObject) {
            if (colliderTag == null || colliderTag.Trim() == "") {
                Debug.LogError("colliderTag needs to be not null or empty");
                return;
            }
            if (other.gameObject.tag == colliderTag)
                buttonIsPushed = true;
        }
    }

    public bool isButtonPushed() {
        return buttonIsPushed;
    }
}
