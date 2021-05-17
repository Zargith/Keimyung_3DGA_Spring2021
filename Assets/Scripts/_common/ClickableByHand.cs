using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickableByHand : MonoBehaviour
{
    private Button m_button;

    private void Awake()
    {
        m_button = GetComponent<Button>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player_hand"))
        {
            m_button.onClick.Invoke();
        }
    }
}
