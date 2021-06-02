using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpeedLimiter : MonoBehaviour
{
    [SerializeField] float MaxSpeed; // Unit per second

    Rigidbody m_rb;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (m_rb.velocity != Vector3.zero)
        {
            m_rb.velocity = m_rb.velocity.normalized * Mathf.Min(m_rb.velocity.magnitude, MaxSpeed);
        }
    }
}
