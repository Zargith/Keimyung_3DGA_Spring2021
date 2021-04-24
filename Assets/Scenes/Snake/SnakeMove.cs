using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    private float speed = 1.5f;
    private float _time = 0.0f;
    private int _increaseSpeed = 5;
    private Transform _toRotate;
    public bool move = true;
    public enum dir {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE
    };

    public enum plane {
        FRONT,
        LEFT,
        BACK,
        RIGHT
    }

    private dir _currDir = dir.NONE;
    public plane _currPlane = plane.FRONT;

    void Start()
    {
        _toRotate = GetComponent<Transform>();
    }

    void Update()
    {
        float timeElapsed = Time.deltaTime;
        _time += timeElapsed;

        if (_time >= _increaseSpeed) {
            //speed += 1.5f;
            _time -= _increaseSpeed;
        }
        ChangeDir();
        if (move) { 
            Move(timeElapsed);
        }
    }

    private void ChangeDir()
    {
        if ((Input.GetKeyDown("z") || Input.GetKeyDown(KeyCode.UpArrow)) && (_currDir != dir.DOWN && _currDir != dir.UP)) {
            _currDir = dir.UP;
            _toRotate.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        if ((Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow)) && (_currDir != dir.DOWN && _currDir != dir.UP)) {
            _currDir = dir.DOWN;
            _toRotate.rotation = Quaternion.Euler(0f, 0f, -90f);
        }
        if ((Input.GetKey("d") || Input.GetKey(KeyCode.LeftArrow)) && (_currDir != dir.RIGHT && _currDir != dir.LEFT)) {
            _currDir = dir.RIGHT;
            _toRotate.rotation = Quaternion.Euler(0f,
                _currPlane == plane.FRONT ? 90f : _currPlane == plane.BACK ? -90f : 0f,
                _currPlane == plane.RIGHT ? 180f : 0f);
        }
        if ((Input.GetKey("q") || Input.GetKey(KeyCode.RightArrow)) && (_currDir != dir.RIGHT && _currDir != dir.LEFT)) {
            _currDir = dir.LEFT;
            _toRotate.rotation = Quaternion.Euler(0f,
                _currPlane == plane.FRONT ? -90f : _currPlane == plane.BACK ? 90f : 0f,
                _currPlane == plane.LEFT ? 180f : 0f);
        }
    }

    private void Move(float timeElapsed)
    {
        Vector3 pos = transform.position;

        switch (_currDir) {
            case dir.UP:
                pos.y += speed * timeElapsed;
                break;
            case dir.DOWN:
                pos.y -= speed * timeElapsed;
                break;
            case dir.RIGHT:
                pos.x = _currPlane == plane.LEFT ? pos.x + speed * timeElapsed : _currPlane == plane.RIGHT ? pos.x - speed * timeElapsed : pos.x;
                pos.z = _currPlane == plane.BACK ? pos.z + speed * timeElapsed : _currPlane == plane.FRONT ? pos.z - speed * timeElapsed : pos.z;
                break;
            case dir.LEFT:
                pos.x = _currPlane == plane.RIGHT ? pos.x + speed * timeElapsed : _currPlane == plane.LEFT ? pos.x - speed * timeElapsed : pos.x;
                pos.z = _currPlane == plane.FRONT ? pos.z + speed * timeElapsed : _currPlane == plane.BACK ? pos.z - speed * timeElapsed : pos.z;
                break;
        }
        transform.position = pos;
    }

    public dir GetDir()
    {
        return _currDir;
    }
}
