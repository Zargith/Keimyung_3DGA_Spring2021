using UnityEngine;

public class LevelGarbageCollecter : MonoBehaviour
{
    [SerializeField] private Transform LevelRoot;

    private DuckHandGameManager m_gameManager;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<DuckHandGameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Garbage collection of " + other.name);

        if (other.transform.IsChildOf(LevelRoot))
        {
            Destroy(other.gameObject); // TODO: cool animation

            if (other.CompareTag("DuckHand_Target"))
                m_gameManager.LoseHealth();
        }
    }
}
