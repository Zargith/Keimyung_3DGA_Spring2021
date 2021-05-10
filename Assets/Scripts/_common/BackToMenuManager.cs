using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuManager : MonoBehaviour
{
    public float JulMaxDelay = 1;
    public float JulRecognitionThreshHold = 0.1f;
    public float JulDistanceThreshHold = 0.15f;

    private GestureProcessor m_gp;
    private float m_julTimer = 0;
    private bool m_areHandTogether = false;

    private GameObject m_leftHand;
    private GameObject m_rightHand;

    private static BackToMenuManager m_instance;

    void Start()
    {
        if (m_instance)
        {
            Destroy(this.gameObject);
            return;
        }
        m_instance = this;

        m_gp = GetComponent<GestureProcessor>();
        InitializeObjectReferences();

        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += (Scene S, LoadSceneMode lsm) => InitializeObjectReferences();
    }

    private void InitializeObjectReferences()
    {
        // TODO: Find a way to not use the name, one should be free to rename without side 
        m_leftHand = GameObject.Find("LeftHandAnchor");
        m_rightHand = GameObject.Find("RightHandAnchor");


        m_gp.LeftHand = m_leftHand.GetComponentInChildren<OVRSkeleton>();
        m_gp.RightHand = m_rightHand.GetComponentInChildren<OVRSkeleton>();
    }

    private void Update()
    {
        m_julTimer = Mathf.Max(m_julTimer - Time.deltaTime, 0);

        if (IsDoingJulSign())
        {
            if (m_julTimer > 0)
                GoBackToMenu();
            else
                m_julTimer = JulMaxDelay;
        }

    }

    private void GoBackToMenu()
    {
        SceneManager.LoadScene("_MainMenu");
    }

    private bool IsDoingJulSign()
    {
        var handDistance = Vector3.Distance(m_leftHand.transform.position, m_rightHand.transform.position);

        if (m_areHandTogether)
        {
            if (handDistance > JulDistanceThreshHold)
                m_areHandTogether = false;
            return false;
        }
        else if (handDistance > JulDistanceThreshHold)
            return false;
        

        m_areHandTogether = true;

        var gestureRight = m_gp.CompareGesture(Hand.Right, "Jul");
        var gestureLeft = m_gp.CompareGesture(Hand.Left, "Jul");

        Debug.Log($"L: {gestureLeft:0.00}, R:  {gestureRight:0.00}");

        return gestureRight > JulRecognitionThreshHold
            && gestureLeft > JulRecognitionThreshHold;
    }
}
