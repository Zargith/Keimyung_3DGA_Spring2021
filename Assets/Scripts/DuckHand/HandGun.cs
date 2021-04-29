using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandGun : MonoBehaviour
{
    public Hand Hand = Hand.Right;
    public GameObject BulletPrefab;
    public GameObject GunPrefab;
    public GameObject OVRHand;
    public GestureProcessor GestureProcessor;
    public Transform ShootToward;


    public bool EnableGunMesh = false;

    public static float ShootCooldownSeconds = 0.3f;
    public static float GunGestureThreshold = 0.2f;
    public static float PitchAccelerationShotThreshold = 10_000f;
    public static float ShotInitialImpulseForce = 100f;

    public static float LazerRayLifetime = 0.5f;


    private static readonly List<string> kGunGesturesName = new List<string> { "Gun_0", "Gun_1" };

    private float m_lastPitch; // degree
    private float m_lastPitchSpeed; // degree per second
    private float m_currentCd = 0f;

    private Ray m_lastShootingRay;
    private Ray? m_LastPlausibleShotDirection;

    private GameObject gunInstance;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(GetShootingRay());
    }

    void Start()
    {
        m_lastPitch = GetCurrentPitch();
        m_lastPitchSpeed = 0;
        m_lastShootingRay = GetShootingRay();
    }

    void FixedUpdate()
    {
        var isGunShape = GetCurrentGunGestureCorrespondance() > GunGestureThreshold;

        var shootRay = GetShootingRay();

        shootRay = HandleShootTrigger(isGunShape, shootRay);


        if (EnableGunMesh)
            HandleGunMesh(isGunShape, shootRay);
    }

    private void HandleGunMesh(bool isGunShape, Ray shootRay)
    {
        // Gun spawn
        if (isGunShape && gunInstance == null)
        {
            OVRHand.GetComponent<SkinnedMeshRenderer>().enabled = false;
            OVRHand.GetComponent<OVRMeshRenderer>().enabled = false;
            gunInstance = Instantiate(GunPrefab, transform);
        }


        // Gun update (also on 1rst tick)
        if (isGunShape)
            gunInstance.GetComponent<TransformRayMatcher>().PositionToMatchRay(shootRay, GetUpDirection());


        // Gun destroy
        if (!isGunShape && gunInstance != null)
        {
            Destroy(gunInstance);
            gunInstance = null;
            OVRHand.GetComponent<SkinnedMeshRenderer>().enabled = true;
            OVRHand.GetComponent<OVRMeshRenderer>().enabled = true;
        }
    }

    private Ray HandleShootTrigger(bool isGunShape, Ray shootRay)
    {
        var pitch = GetCurrentPitch();
        var pitchSpeed = (pitch - m_lastPitch) / Time.deltaTime;
        var pitchAcceleration = (pitchSpeed - m_lastPitchSpeed) / Time.deltaTime;

        if (pitchSpeed > 0 && m_lastPitchSpeed < 0)
        {
            var lerpVal = Mathf.InverseLerp(m_lastPitchSpeed, pitchSpeed, 0);

            m_LastPlausibleShotDirection = new Ray(
                Vector3.Lerp(m_lastShootingRay.origin, shootRay.origin, lerpVal),
                Vector3.Lerp(m_lastShootingRay.direction, shootRay.direction, lerpVal).normalized
            );
        }

        if (m_currentCd > 0)
        {
            m_currentCd -= Time.deltaTime;
        }
        else if (isGunShape
            && pitchAcceleration > PitchAccelerationShotThreshold
            && m_LastPlausibleShotDirection.HasValue)
        {
            DoShoot();
        }

        m_lastPitch = pitch;
        m_lastPitchSpeed = pitchSpeed;
        m_lastShootingRay = GetShootingRay();
        return shootRay;
    }

    private void DoShoot()
    {
        Ray shootRay = m_LastPlausibleShotDirection.Value;

        var ball = Instantiate(BulletPrefab, shootRay.origin, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(shootRay.direction * ShotInitialImpulseForce, ForceMode.Impulse);

        RaycastHit rayCast;
        Physics.Raycast(shootRay, out rayCast);

        GetComponent<LineRenderer>().SetPosition(0, shootRay.origin);
        GetComponent<LineRenderer>().SetPosition(1, rayCast.point != Vector3.zero ? rayCast.point : shootRay.origin + shootRay.direction * 9999);
        GetComponent<LineRenderer>().enabled = true;

        Invoke(nameof(HideLazer), LazerRayLifetime);

        m_currentCd = ShootCooldownSeconds;
        m_LastPlausibleShotDirection = null;
    }

    void HideLazer()
    {
        GetComponent<LineRenderer>().enabled = false;
    }

    /// <summary>
    /// How close the hand gesture looks like a gun
    /// </summary>
    /// <returns>[0, 1], the higher the better</returns>
    float GetCurrentGunGestureCorrespondance()
    {
        return kGunGesturesName.Max(gesture => GestureProcessor.CompareGesture(Hand, gesture));
    }

    /// <summary>
    /// NOTE: Get inverted if gun is turned upside down
    /// </summary>
    float GetCurrentPitch()
    {
        var shootDir = GetShootingRay().direction;
        var horizontal_forward = new Vector3(shootDir.x, 0, shootDir.z);

        float currentPitch = Vector3.Angle(horizontal_forward, GetUpDirection());

        return currentPitch;
    }

    Vector3 GetUpDirection()
    {
        if (Hand == Hand.Right)
            return -transform.forward;
        else
            return transform.forward;
    }

    Ray GetShootingRay()
    {
        var result = new Ray
        {
            origin = transform.position,
            direction = (ShootToward.position - transform.position).normalized
        };


        var skeleton = OVRHand.GetComponent<OVRSkeleton>();
        if (skeleton.Bones != null && skeleton.Bones.Any())
        {
            var indexBase = OVRHand.GetComponent<OVRSkeleton>().Bones[(int)OVRSkeleton.BoneId.Hand_Index1].Transform.position;
            var indexTip = OVRHand.GetComponent<OVRSkeleton>().Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;

            result.origin = indexBase;
            result.origin = indexBase;
            result.direction = Vector3.Lerp(result.direction, (indexTip - indexBase).normalized, 0.5f);
        }

        return result;
    }
}
