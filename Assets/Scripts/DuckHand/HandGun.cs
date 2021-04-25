using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : MonoBehaviour
{
    public Hand Hand = Hand.Right;
    public GameObject BulletPrefab;

    public static float ShootCooldownSeconds = 0.3f;
    public static float GunGestureThreshold = 0.1f;
    public static float DeltaPitchShotThreshold = 1f;
    public static float ShotInitialImpulseForce = 10f;

    private float _lastPitch;
    private float _currentCd = 0f;

    private GestureProcessor _gp;


    void Start()
    {
        _lastPitch = GetCurrentPitch();
        _gp = FindObjectOfType<GestureProcessor>();

        Debug.Log("NEW SIMULATION ########################################################################################");
    }

    float GetCurrentPitch()
    {
        var horizontal_forward = GetShootingDirection();
        horizontal_forward.y = 0;

        float currentPitch = Vector3.Angle(horizontal_forward, transform.up);

        return currentPitch;
    }

    Vector3 GetShootingDirection()
    {
        if (Hand == Hand.Right)
            return -transform.right;
        else
            return transform.right;
    }

    void FixedUpdate()
    {
        var gunGestureCorrespondance = _gp.CompareGesture(Hand.Right, "Gun");


        var pitch = GetCurrentPitch();
        var deltaPitch = pitch - _lastPitch;

        _lastPitch = pitch;


        Debug.Log("Gun correspondance : " + gunGestureCorrespondance);
        Debug.Log("Pitch delta : " + deltaPitch);
        //Debug.Log("smart : " + smart);


        if (_currentCd > 0)
        {
            _currentCd -= Time.deltaTime;
        }
        else if (gunGestureCorrespondance > GunGestureThreshold && deltaPitch > DeltaPitchShotThreshold)
        {
            var ball = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(GetShootingDirection() * ShotInitialImpulseForce, ForceMode.Impulse);

            _currentCd = ShootCooldownSeconds;
        }
    }
}
