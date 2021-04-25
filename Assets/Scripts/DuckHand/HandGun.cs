using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandGun : MonoBehaviour
{
    public Hand Hand = Hand.Right;
    public GameObject BulletPrefab;

    public static float ShootCooldownSeconds = 0.3f;
    public static float GunGestureThreshold = 0.2f;
    public static float DeltaPitchShotThreshold = 1f;
    public static float ShotInitialImpulseForce = 10f;

    private static readonly List<string> kGunGesturesName = new List<string> { "Gun_0", "Gun_1" };

    private float m_lastPitch;
    private float m_lastDeltaPitch;
    private float m_currentCd = 0f;

    private GestureProcessor m_gp;

    private Vector3 m_lastShotDirection;
    private Vector3? m_LastPlausibleShotDirection;

    void Start()
    {
        m_lastPitch = GetCurrentPitch();
        m_lastDeltaPitch = 0;
        m_lastShotDirection = GetShootingDirection();

        m_gp = FindObjectOfType<GestureProcessor>();

        Debug.Log("NEW SIMULATION ########################################################################################");
    }

    void FixedUpdate()
    {
        var gunGestureCorrespondance = GetCurrentGunGestureCorrespondance();

        var pitch = GetCurrentPitch();
        var deltaPitch = pitch - m_lastPitch;


        if (deltaPitch > 0 && m_lastDeltaPitch < 0)
            m_LastPlausibleShotDirection = Vector3.Lerp(m_lastShotDirection, GetShootingDirection(), Mathf.InverseLerp(m_lastDeltaPitch, deltaPitch, 0));

        if (m_currentCd > 0)
        {
            m_currentCd -= Time.deltaTime;
        }
        else if (gunGestureCorrespondance > GunGestureThreshold 
            && deltaPitch > DeltaPitchShotThreshold
            && m_LastPlausibleShotDirection.HasValue)
        {
            var ball = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(m_LastPlausibleShotDirection.Value * ShotInitialImpulseForce, ForceMode.Impulse);

            m_currentCd = ShootCooldownSeconds;
            m_LastPlausibleShotDirection = null;
        }

        m_lastPitch = pitch;
        m_lastDeltaPitch = deltaPitch;
        m_lastShotDirection = GetShootingDirection();
    }


    /// <summary>
    /// How close the hand gesture looks like a gun
    /// </summary>
    /// <returns>[0, 1], the higher the better</returns>
    float GetCurrentGunGestureCorrespondance()
    {
        return kGunGesturesName.Max(gesture => m_gp.CompareGesture(Hand, gesture));
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
}
