using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float ShootCooldownSeconds = 0.3f;
    public float ThreshHold = 5f;
    
    private Quaternion _lastRotation;
    private float _currentCd = 0f;

    private float _lastSmart = 0;

    void Start()
    {
        _lastRotation = transform.rotation;

        Debug.Log("NEW SIMULATION ########################################################################################");
    }

    void FixedUpdate()
    {
        var delta = GetQuaternionDiff(transform.rotation, _lastRotation);
        var deltaEuler = delta.eulerAngles;

        _lastRotation = transform.rotation;

        if (deltaEuler.x > 180)
            deltaEuler.x -= 360;
        if (deltaEuler.y > 180)
            deltaEuler.y -= 360;
        if (deltaEuler.z > 180)
            deltaEuler.z -= 360;

        var smart = Mathf.Sqrt(deltaEuler.x * deltaEuler.x + deltaEuler.z * deltaEuler.z) / Mathf.Max(1f, Mathf.Abs(deltaEuler.y));
        var deltasmart = (smart - _lastSmart);

        _lastSmart = smart;

        Debug.Log("Right Delta x : " + deltaEuler.x);
        Debug.Log("Right Delta y : " + deltaEuler.y);
        Debug.Log("Right Delta z : " + deltaEuler.z);


        Debug.Log("Rotation delta : " + Quaternion.Angle(Quaternion.identity, delta));

        Debug.Log("smart : " + smart);
        Debug.Log("DeltaSmart : " + deltasmart);


        if (_currentCd > 0)
        {
            _currentCd -= Time.deltaTime;
            return;
        }
        if (_currentCd < 0)
            _currentCd = 0f;


        if (deltasmart > 4)
        {
            Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            _currentCd = ShootCooldownSeconds;
        }
    }

    Quaternion GetQuaternionDiff(Quaternion a, Quaternion b)
    {
        return a * Quaternion.Inverse(b);
    }
}
