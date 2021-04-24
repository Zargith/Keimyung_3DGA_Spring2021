using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    [SerializeField] private GameObject BackToLeft = null;
    [SerializeField] private GameObject BackToRight = null;
    [SerializeField] private GameObject FrontToLeft = null;
    [SerializeField] private GameObject FrontToRight = null;
    [SerializeField] private GameObject LeftToBack = null;
    [SerializeField] private GameObject RightToBack = null;
    [SerializeField] private GameObject LeftToFront = null;
    [SerializeField] private GameObject RightToFront = null;

    private Vector3 prevPos = Vector3.zero;

    //private SnakeMovement.plane plane = SnakeMovement.plane.RIGHT;
    private SnakeMovement.plane plane = SnakeMovement.plane.FRONT;
    public enum dir
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE
    };

    private dir _currDir = dir.NONE;

    // Update is called once per frame
    void Update()
    {
        if (plane == SnakeMovement.plane.FRONT) {
            _currDir = transform.position.z >= prevPos.z ? dir.LEFT : dir.RIGHT;
        } else if (plane == SnakeMovement.plane.BACK) {
            _currDir = transform.position.z <= prevPos.z ? dir.LEFT : dir.RIGHT;
        } else if (plane == SnakeMovement.plane.LEFT) {
            _currDir = transform.position.x <= prevPos.x ? dir.LEFT : dir.RIGHT;
        } else if (plane == SnakeMovement.plane.RIGHT)
            _currDir = transform.position.x >= prevPos.x ? dir.LEFT : dir.RIGHT;

        prevPos = transform.position;
        //Debug.Log("dir : " + _currDir);

        switch (plane) {
            case SnakeMovement.plane.BACK:
                if (_currDir == dir.RIGHT && transform.position.z >= BackToLeft.transform.position.z) {
                    Debug.Log("back to left");

                    plane = SnakeMovement.plane.LEFT;
                }
                else if (_currDir == dir.LEFT && transform.position.z <= BackToRight.transform.position.z) {
                    Debug.Log("back to right");

                plane = SnakeMovement.plane.RIGHT;
                }
                break;
            case SnakeMovement.plane.FRONT:
                if (_currDir == dir.LEFT && transform.position.z >= FrontToLeft.transform.position.z) {
                    Debug.Log("front to left");

                plane = SnakeMovement.plane.LEFT;
                }
                else if (_currDir == dir.RIGHT && transform.position.z <= FrontToRight.transform.position.z) {
                    Debug.Log("front to right");

                    plane = SnakeMovement.plane.RIGHT;
                }
                break;
            case SnakeMovement.plane.LEFT:
                if (_currDir == dir.LEFT && transform.position.x <= LeftToBack.transform.position.x) {
                    Debug.Log("left to back");

                plane = SnakeMovement.plane.BACK;
                }
                else if (_currDir == dir.RIGHT && transform.position.x >= LeftToFront.transform.position.x) {
                    Debug.Log("left to front");

                plane = SnakeMovement.plane.FRONT;
                }
                break;
            case SnakeMovement.plane.RIGHT:
                if (_currDir == dir.RIGHT && transform.position.x <= RightToBack.transform.position.x) {
                    Debug.Log("right to back");
                plane = SnakeMovement.plane.BACK;

                }
                else if (_currDir == dir.LEFT && transform.position.x >= RightToFront.transform.position.x) {
                    Debug.Log("right to front");
                    plane = SnakeMovement.plane.FRONT;
                }
                break;
        }
    }

    public SnakeMovement.plane GetBodyDir()
    {
        return plane;
    }
}
