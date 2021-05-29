using UnityEngine;
using UnityEngine.UI;

public class ClickableByShoot : MonoBehaviour, IShootable
{
    void IShootable.OnHit()
    {
        GetComponent<Button>().onClick.Invoke();
    }
}
