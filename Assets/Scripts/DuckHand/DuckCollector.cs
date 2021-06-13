using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DuckCollector : MonoBehaviour
{
    [SerializeField] private DuckHandGameManager m_gameManager;
    private AudioSource m_audioSource;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DuckHand_Target") && other.GetComponent<Rigidbody>().velocity.y < 0)
        {
            m_gameManager.LoseHealth();
            Destroy(other.gameObject);
            m_audioSource.Play();
        }
    }
}
