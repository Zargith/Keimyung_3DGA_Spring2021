using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    public Vector3 Direction = Vector3.forward;
    public float InitialSpeed = 1f;
    public float Acceleration = 1f;

    private float m_currentSpeed;
    private bool m_isScrolling = false;

    public void StartScrolling()
    {
        m_isScrolling = true;
        ResetScrollingSpeed();
    }

    public void StopScrolling()
    {
        m_isScrolling = false;
    }

    public void ResetScrollingSpeed()
    {
        m_currentSpeed = InitialSpeed;
    }

    void Update()
    {
        if (m_isScrolling)
        {
            m_currentSpeed += Acceleration * Time.deltaTime;
            transform.Translate(Direction.normalized * m_currentSpeed * Time.deltaTime);
        }
    }
}
